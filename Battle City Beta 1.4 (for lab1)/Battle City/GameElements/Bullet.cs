using System;

namespace Battle_City
{
    class Bullet : BaseObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private readonly string _bulletFigure;
        private readonly int _dx;
        private readonly int _dy;

        public int OldX { get; private set; }
        public int OldY { get; private set; }

        public Bullet(int x, int y, int dx, int dy, string symbol) : base(x, y, symbol)
        {
            X = x;
            Y = y;
            _dx = dx;
            _dy = dy;
            _bulletFigure = symbol;
        }

        public void Move()
        {
            X += _dx;
            Y += _dy;
        }



        public override void Display()
        {
            Console.SetCursorPosition(OldX, OldY);
            Console.Write(" ");
            Console.SetCursorPosition(X, Y);
            Console.Write(_bulletFigure);
        }

        public void DrawToBuffer(char[,] buffer)
        {
            int bufferX = X;
            int bufferY = Y;
            if (bufferX >= 0 && bufferX < buffer.GetLength(0) && bufferY >= 0 && bufferY < buffer.GetLength(1))
            {
                buffer[bufferX, bufferY] = Symbol[0];
                if (Symbol.Length > 1 && bufferX < buffer.GetLength(0))
                    buffer[bufferX, bufferY] = Symbol[1];
            }
        }


        public void Remove(List<Bullet> bullets)
        {

            foreach (var bullet in bullets)
            {
                bullets.Remove(bullet);
            }
        }

        public static void UpdateBullets(List<Bullet> bullets, Field _field, Reward _reward, Enemy enemy)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                bullet.OldX = bullet.X;
                bullet.OldY = bullet.Y;
                bullet.Move();

                if (bullet.X < 0 || bullet.X >= Field.Width || bullet.Y < 0 || bullet.Y >= Field.Height ||
            !_field.IsWalkable(bullet.X, bullet.Y) || _reward.IsReward(bullet.X, bullet.Y))
                {
                    Console.SetCursorPosition(bullet.OldX, bullet.OldY);
                    Console.Write(" ");
                    bullets.RemoveAt(i); 
                }
                else
                {
                    if (bullet.X != enemy.X && bullet.Y != enemy.Y)
                        bullet.Display(); 
                }
            }
        }

        public static void HitPlayer(List<Bullet> bullets, Player _player)
        {
            foreach (var bullet in bullets)
            {
                if (bullet._bulletFigure == Enemy.EnemyBullet)
                {
                    if (_player.X == bullet.X && _player.Y == bullet.Y)
                    {
                        Game.Lives--;
                        if (Game.Lives == 0)
                        {
                            Game.GameState = Game.State.Pause;
                            break;
                        }
                        bullets.Remove(bullet);
                        break;
                    }
                }
            }
        }

        public static void HitEnemy(List<Bullet> bullets, Enemy _enemy, Field _field)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                if (bullet._bulletFigure == Player.PlayerBullet)
                {
                    if (_enemy.X == bullet.X && _enemy.Y == bullet.Y)
                    {
                        
                        Console.SetCursorPosition(_enemy.X, _enemy.Y);
                        Console.Write(" ");

                        bullets.RemoveAt(i);

                        _enemy.X = Game.Random.Next(0, Field.Width);
                        _enemy.Y = Game.Random.Next(0, Field.Height);

                        while (!_field.IsWalkable(_enemy.X, _enemy.Y))
                        {
                            _enemy.X = Game.Random.Next(0, Field.Width);
                            _enemy.Y = Game.Random.Next(0, Field.Height);
                        }

                        _enemy.Display();
                        break; 
                    }
                }
            }
        }
    }
}

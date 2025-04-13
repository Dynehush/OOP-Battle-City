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
            Console.SetCursorPosition(OldX * 2, OldY);
            Console.Write("  ");
            Console.SetCursorPosition(X * 2, Y);
            Console.Write(_bulletFigure);
        }

        public void Remove(List<Bullet> bullets)
        {

            foreach (var bullet in bullets)
            {
                bullets.Remove(bullet);
            }
        }

        public static void UpdateBullets(List<Bullet> bullets, Field _field, Reward _reward)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                bullet.OldX = bullet.X;
                bullet.OldY = bullet.Y;
                bullet.Move();

                if (bullet.X < 0 || bullet.X >= Field.WIDTH || bullet.Y < 0 || bullet.Y >= Field.HEIGHT ||
            !_field.IsWalkable(bullet.X, bullet.Y) || _reward.IsReward(bullet.X, bullet.Y))
                {
                    Console.SetCursorPosition(bullet.OldX * 2, bullet.OldY);
                    Console.Write("  ");
                    bullets.RemoveAt(i); 
                }
                else
                {
                    bullet.Display(); 
                }
            }
        }

        public static void HitPlayer(List<Bullet> bullets, Player _player)
        {
            foreach (var bullet in bullets)
            {
                if (bullet._bulletFigure == Enemy._enemyBullet)
                {
                    if (_player.X == bullet.X && _player.Y == bullet.Y)
                    {
                        Game.GameState = Game.State.Pause;
                        Menu.ShowGameOver();
                    }
                }
            }
        }

        public static void HitEnemy(List<Bullet> bullets, Enemy _enemy, Field _field)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                if (bullet._bulletFigure == Player._playerBullet)
                {
                    if (_enemy.X == bullet.X && _enemy.Y == bullet.Y)
                    {
                        
                        Console.SetCursorPosition(_enemy.X * 2, _enemy.Y);
                        Console.Write("  ");

                        bullets.RemoveAt(i);

                        _enemy.X = Game.Random.Next(0, Field.WIDTH);
                        _enemy.Y = Game.Random.Next(0, Field.HEIGHT);

                        while (!_field.IsWalkable(_enemy.X, _enemy.Y))
                        {
                            _enemy.X = Game.Random.Next(0, Field.WIDTH);
                            _enemy.Y = Game.Random.Next(0, Field.HEIGHT);
                        }

                        _enemy.Display();
                        break; 
                    }
                }
            }
        }



    }
}

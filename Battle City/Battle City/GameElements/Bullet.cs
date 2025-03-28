using System;

namespace SomeWeirdGame
{
    class Bullet : Game
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private readonly string _bulletFigure;
        //private readonly string _playerBulletFigure = "🔴";
        private readonly int _dx;
        private readonly int _dy;

        public Bullet(int x, int y, int dx, int dy, string symbol)
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
                bullet.Move();    

                if (bullet.X >= 0 && bullet.X < Field.WIDTH &&
                    bullet.Y >= 0 && bullet.Y < Field.HEIGHT)
                {
                    if (!_field.IsWalkable(bullet.X, bullet.Y) || _reward.IsReward(bullet.X, bullet.Y))
                    {
                        bullets.RemoveAt(i);
                    }
                    else
                    {
                        bullet.Display();
                    }
                }
                else
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        public static void Hit(List<Bullet> bullets, Player _player)
        {
            foreach(var bullet in bullets)
            {
                if (_player.X == bullet.X && _player.Y == bullet.Y)
                {
                    GameState = State.Defeat;
                }
            }
        }

        
    }
}

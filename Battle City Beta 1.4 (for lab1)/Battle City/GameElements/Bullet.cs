

namespace Battle_City
{
    class Bullet : BaseObject
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private readonly string _bulletFigure;
        private readonly int _dx;
        private readonly int _dy;

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

        public override void DrawToBuffer(char[,] buffer)
        {
            buffer[X, Y] = Symbol[0];
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
                bullet.Move();

                if (bullet.X < 0 || bullet.X >= Field.Width || bullet.Y < 0 || bullet.Y >= Field.Height ||
            !_field.IsWalkable(bullet.X, bullet.Y) || _reward.IsReward(bullet.X, bullet.Y))
                {
                    bullets.RemoveAt(i); 
                }
            }
        }

        public static void HitPlayer(List<Bullet> bullets, Player _player)
        {

            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                if (bullet._bulletFigure == Enemy.EnemyBullet &&
                    _player.X == bullet.X && _player.Y == bullet.Y)
                {
                    Game.Lives--;
                    if (Game.Lives == 0)
                    {
                        Game.GameState = Game.State.Pause;
                        Menu.ShowGameOver();
                    }
                    bullets.RemoveAt(i);
                    break;
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

                        bullets.RemoveAt(i);
                        _enemy.Respawn(_field, _enemy);
                        break;
                    }
                }
            }
        }
    }
}

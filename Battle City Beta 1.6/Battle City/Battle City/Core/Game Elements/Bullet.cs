using Battle_City.Core.Actor;
using Battle_City.Core;
using static Battle_City.Core.Actor.Entity;

namespace Battle_City.Core.GameElements
{
    public class Bullet : BaseObject
    {
        private readonly int _dx;
        private readonly int _dy;

        public Bullet(int x, int y, int dx, int dy) : base(x, y)
        {
            X = x;
            Y = y;
            _dx = dx;
            _dy = dy;
        }

        public void Move()
        {
            X += _dx;
            Y += _dy;
        }
        public bool Hits(Entity entity)
        {
            return X == entity.X && Y == entity.Y;
        }

        public void Remove(List<Bullet> bullets)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets.RemoveAt(i);
            }
        }

        public static void UpdateBullets(List<Bullet> bullets, Field _field, Player _player, Enemy _enemy)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                bullet.Move();

                if (bullet.IsOutOfBounds(_field) ||
                    Reward.IsReward(bullet.X, bullet.Y) ||
                    !_field[bullet.X, bullet.Y].IsWalkable())
                {
                    bullets.RemoveAt(i);
                    continue;
                }

                if (bullet.Hits(_player))
                {
                    _player.OnHit();
                    bullets.RemoveAt(i);
                    continue;
                }

                if (bullet.Hits(_enemy))
                {
                    _enemy.OnHit(_field);
                    bullets.RemoveAt(i);
                    continue;
                }
            }
        }
        public static void CheckBulletOverlap(Entity entity, List<Bullet> enemyBullets)
        {
            var bulletsToRemove = new List<Bullet>();

            foreach (Bullet bullet in enemyBullets)
            {
                if (bullet.X == entity.X && bullet.Y == entity.Y)
                {
                    entity.OnHit();
                    bulletsToRemove.Add(bullet);
                }
            }

            foreach (Bullet bullet in bulletsToRemove)
            {
                entity.Bullets.Remove(bullet);
            }
        }
    }
}

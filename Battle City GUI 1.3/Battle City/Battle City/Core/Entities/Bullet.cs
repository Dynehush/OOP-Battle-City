using static Battle_City.Core.Entities.Actor;
using Battle_City.Core.Map;


namespace Battle_City.Core.Entities
{
    public class Bullet : BaseObject
    {
        private readonly int _dx;
        private readonly int _dy;
        public Direction? Direction;
        public static List<Bullet> Bullets { get; set; } = new List<Bullet>();
        public Actor Owner { get; }

        public Bullet(int x, int y, int dx, int dy, Direction? direction, Actor owner) : base(x, y)
        {
            X = x;
            Y = y;
            _dx = dx;
            _dy = dy;
            Direction = direction;
            Owner = owner;
        }

        private void Move()
        {
            X += _dx;
            Y += _dy;
        }
        private bool Hits(Actor entity)
        {
            return X == entity.X && Y == entity.Y;
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

                if (bullet.Hits(_player) && bullet.Owner != _player)
                {
                    _player.OnHit();
                    bullets.RemoveAt(i);
                    continue;
                }

                if (bullet.Hits(_enemy) && bullet.Owner != _enemy)
                {
                    _enemy.OnHit(_field);
                    bullets.RemoveAt(i);
                    continue;
                }
            }
        }

        public static void CheckBulletOverlap(Field _field, Actor entity, List<Bullet> bullets)
        {
            var bulletsToRemove = new List<Bullet>();

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Hits(entity))
                {
                    if (entity is Player _player && bullets[i].Owner != _player)
                    {
                        _player.OnHit();
                        bulletsToRemove.Add(bullets[i]);
                    }
                    else if (entity is Enemy _enemy && bullets[i].Owner != _enemy)
                    {
                        _enemy.OnHit(_field);
                        bulletsToRemove.Add(bullets[i]);
                    }
                }
            }

            for (int i = 0; i < bulletsToRemove.Count; i++)
            {
                bullets.Remove(bulletsToRemove[i]);
            }
        }
    }
}

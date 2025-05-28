using Battle_City.Core.GameElements;
using System.Collections.Generic;
using Battle_City;


namespace Battle_City.Core.Actor
{
    public class Entity : BaseObject
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        public Dictionary<Direction, (int dx, int dy)> DirectionOffsets = new()
        {
            [Direction.Up] = (0, -1),
            [Direction.Down] = (0, 1),
            [Direction.Left] = (-1, 0),
            [Direction.Right] = (1, 0)
        };
        public List<Bullet> Bullets { get; } = new List<Bullet>();
        public int Health { get; set; }

        public Entity(int x, int y, int health = int.MaxValue) : base(x, y)
        {
            Health = health;
        }

        public virtual void Shoot(Entity entity)
        {
            int dx = Math.Sign(entity.X - X);
            int dy = Math.Sign(entity.Y - Y);
            if (dx == 0 || dy == 0)
            {
                Bullets.Add(new Bullet(X, Y, dx, dy));
            }
        }

        public virtual void OnHit()
        {
            Health--;
        }
        public bool IsAtPosition(int x, int y)
        {
            return X == x && Y == y;
        }
        public Direction GetDirectionToEntity(Player player)
        {
            int dx = player.X - this.X;
            int dy = player.Y - this.Y;

            if (Math.Abs(dx) > Math.Abs(dy))
                return dx > 0 ? Direction.Right : Direction.Left;
            else
                return dy > 0 ? Direction.Down : Direction.Up;
        }
    }
}


using Battle_City.Core.Map;

namespace Battle_City.Core.Entities
{
    public class Actor : BaseObject
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
        public int Health { get; set; }

        public Actor(int x, int y, int health = int.MaxValue) : base(x, y)
        {
            Health = health;
        }

        public virtual void Shoot(Direction? direction, Field _field)
        {
            if (direction.HasValue) 
            {
                var (dx, dy) = DirectionOffsets[direction.Value];
                int newX = X + dx;
                int newY = Y + dy;
                if (_field[newX, newY].IsWalkable() && !IsOutOfBounds(_field)) 
                    Bullet.Bullets.Add(new Bullet(newX, newY, dx, dy, direction, this));
            }
        }
        public bool IsAtPosition(int x, int y)
        {
            return X == x && Y == y;
        }
        public Direction GetDirectionToEntity(Player player)
        {
            int dx = player.X - X;
            int dy = player.Y - Y;

            if (Math.Abs(dx) > Math.Abs(dy))
                return dx > 0 ? Direction.Right : Direction.Left;
            else
                return dy > 0 ? Direction.Down : Direction.Up;
        }
    }
}

using Battle_City.Core.GameElements;
using Battle_City.Components;

namespace Battle_City.Core.Actor
{
    public class Player : Entity
    {

        internal DateTime LastShootTime = DateTime.MinValue;
        internal DateTime LastMoveTime = DateTime.MinValue;
        public Direction? PlayerDirection { get; set; } = Direction.Right;

        public Player(Field _field) : base(0, 0, 5)
        {
            var spawnPos = _field.ReachableCells
                                .OrderBy(cell => cell.Item2)
                                .ThenBy(cell => cell.Item1)
                                .FirstOrDefault();

            if (spawnPos == default)
                throw new InvalidOperationException("There is no available position to set a player on.");

            X = spawnPos.Item1;
            Y = spawnPos.Item2;
        }

        public void Move(int dx, int dy, Field _field)
        {
            int newX = X + dx;
            int newY = Y + dy;

            if (IsOutOfBounds(_field))
            {
                throw new ArgumentOutOfRangeException("Player cannot move outside the field.");
            }

            if (_field[newX, newY].IsWalkable())
            {
                X = newX;
                Y = newY;
            }
        }

        public override void OnHit()
        {
            base.OnHit();
            if (Health <= 0)
            {
                Game.GameState = Game.State.Pause;
            }
        }
    }
}

using Battle_City.Core.Actor;
using static Battle_City.Core.Actor.Entity;

namespace Battle_City.Components
{
    public static class Symbols
    {
        public const string PlayerFigure = "→";
        public const string EnemyFigure = "❯";
        public const string BulletFigure = "•";
        public const string BaseFigure = " ";
        public const string BarrierFigure = "#";
        public const string RewardFigure = "@";
        public const string HeartFIgure = "❤️";

        public static readonly Dictionary<Direction, string> PlayerDirection = new()
        {
            { Direction.Up, "↑" },
            { Direction.Down, "↓" },
            { Direction.Left, "←" },
            { Direction.Right, "→" }
        };
    }
}


using static Battle_City.Core.Entities.Actor;

namespace Battle_City.Rendering
{
    class Images
    {
        private static readonly string _imagePath = @"C:\Uni 2 semester\OOP\projects\Battle city GUI 1.2\Battle City\Battle City\images\";

        public static Dictionary<Direction, Image> PlayerDirection = new()
        {
            { Direction.Up, Image.FromFile(Path.Combine(_imagePath, "PlayerUp.png"))},
            { Direction.Down, Image.FromFile(Path.Combine(_imagePath, "PlayerDown.png"))},
            { Direction.Left, Image.FromFile(Path.Combine(_imagePath, "PlayerLeft.png"))},
            { Direction.Right, Image.FromFile(Path.Combine(_imagePath, "PlayerRight.png"))}
        };

        public static Dictionary<Direction, Image> EnemyDirection = new()
        {
            { Direction.Up, Image.FromFile(Path.Combine(_imagePath, "EnemyUp.png"))},
            { Direction.Down, Image.FromFile(Path.Combine(_imagePath, "EnemyDown.png"))},
            { Direction.Left, Image.FromFile(Path.Combine(_imagePath, "EnemyLeft.png"))},
            { Direction.Right, Image.FromFile(Path.Combine(_imagePath, "EnemyRight.png"))}
        };

        public static Dictionary<Direction, Image> BulletDirection = new()
        {
            { Direction.Up, Image.FromFile(Path.Combine(_imagePath, "BulletUp.png"))},
            { Direction.Down, Image.FromFile(Path.Combine(_imagePath, "BulletDown.png"))},
            { Direction.Left, Image.FromFile(Path.Combine(_imagePath, "BulletLeft.png"))},
            { Direction.Right, Image.FromFile(Path.Combine(_imagePath, "BulletRight.png"))}
        };

        public static Image BulletFigure = BulletDirection[Direction.Right]; 
        public static Image PlayerFigure = PlayerDirection[Direction.Right];
        public static Image EnemyFigure = EnemyDirection[Direction.Right];

        public static Image RewardFigure = Image.FromFile(Path.Combine(_imagePath, "Star.png"));
        public static Image BarrierFigure = Image.FromFile(Path.Combine(_imagePath, "Bricks.png"));
        public static Image EmptyFigure = Image.FromFile(Path.Combine(_imagePath, "EmptyObject.png"));
    }
}

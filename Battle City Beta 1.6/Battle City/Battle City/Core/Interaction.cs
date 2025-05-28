

namespace Battle_City.Core.Actor
{
    class Interaction
    {
        public static void CheckCollision(Entity a, Entity b)
        {
            if (a.X == b.X && a.Y == b.Y)
            {
                Game.GameState = Game.State.Pause;
            }
        }
        public static bool IsNearEntity(int x, int y, Entity entity)
        {
            int dx = Math.Abs(entity.X - x);
            int dy = Math.Abs(entity.Y - y);

            return dx <= 2 && dy <= 2;
        }
    }
}

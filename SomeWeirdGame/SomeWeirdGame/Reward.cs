using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace SomeWeirdGame
{
    class Reward
    {
        public static readonly string RewardSign = "⭐"; // Star
        private static readonly Random random = new Random();
        public int NumberOfRewards { get; }
        public Field Field { get; }
        private readonly HashSet<(int, int)> rewardPositions = new HashSet<(int, int)>();

        public Reward(Field field, int number)
        {
            Field = field;
            NumberOfRewards = number;
        }

        public void SetRewards()
        {
            for (int i = 0; i < NumberOfRewards; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(Field.WIDTH / 5, Field.WIDTH);
                    y = random.Next(Field.HEIGHT / 5, Field.HEIGHT);
                } while (!Field.IsWalkable(x, y) || rewardPositions.Contains((x, y)));

                Field.fieldGrid[y, x] = RewardSign;
                rewardPositions.Add((x, y));
            }
        }

        public bool IsReward(int x, int y)
        {
            return rewardPositions.Contains((x, y));
        }

        public int GetReward(int x, int y)
        {
            if (IsReward(x, y))
            {
                Field.fieldGrid[y, x] = Field.FIELD_BASE;
                rewardPositions.Remove((x, y));
                return 1;
            }
            return 0;
        }
    }
}

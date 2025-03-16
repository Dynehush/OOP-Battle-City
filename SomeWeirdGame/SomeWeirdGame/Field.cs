using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeWeirdGame
{
    class Field
    {
        public const int WIDTH = 20;
        public const int HEIGHT = 20;
        public const string FIELD_BASE = "🟦";
        private const string FIELD_BARRIER = "🟥";
        private static readonly Random random = new Random();
        public readonly int NumberOfLevels = 3;

        public string[,] fieldGrid { get; } = new string[HEIGHT, WIDTH];

        public Field()
        {
            GenerateField();
        }

        private void GenerateField()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    if (i == 0 || i == HEIGHT - 1 || j == 0 || j == WIDTH - 1)
                    {
                        fieldGrid[i, j] = FIELD_BARRIER;
                    }
                    else if (random.NextDouble() < 0.17)
                    {
                        fieldGrid[i, j] = FIELD_BARRIER;
                    }
                    else
                    {
                        fieldGrid[i, j] = FIELD_BASE;
                    }
                }
            }
        }

        public void PrintField()
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    sb.Append(fieldGrid[i, j]);
                }
                sb.AppendLine();
            }
            Console.Write(sb.ToString());
        }

        public bool IsWalkable(int x, int y)
        {
            return fieldGrid[y, x] == FIELD_BASE
                || fieldGrid[y, x] == Reward.RewardSign;
        }
    }
}

using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace Battle_City
{
    class Field
    {
        public const int WIDTH = 40;
        public const int HEIGHT = 40;
        public const string FIELD_BASE = "  ";
        private const string FIELD_BARRIER = "🧱";
        public const int NUMBER_OF_LEVELS = 3;
        private int _level { get; }

        public BaseObject[,] FieldGrid { get; } = new BaseObject[WIDTH, HEIGHT];

        public Field(int level)
        {
            _level = level;
            GenerateField();
        }

        public BaseObject this[int x, int y]
        {
            get => FieldGrid[x, y];
            set => FieldGrid[x, y] = value;
        }

        private void GenerateField()
        {
            double possibility = 0.30 * (_level * 0.3);
            if (possibility > 0.5)
            {
                possibility = 0.5;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    if (i == 0 || i == WIDTH - 1 || j == 0 || j == HEIGHT - 1)
                    {
                        FieldGrid[i, j] = new BarrierObject(i, j, FIELD_BARRIER);
                    }
                    else if (Game.Random.NextDouble() < possibility)
                    {
                        FieldGrid[i, j] = new BarrierObject(i, j, FIELD_BARRIER);
                    }
                    else
                    {
                        FieldGrid[i, j] = new EmptyObject(i, j, FIELD_BASE);
                    }
                }
            }
        }

        public void DisplayField()
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < HEIGHT; j++)
            {
                for (int i = 0; i < WIDTH; i++)
                {
                    sb.Append(FieldGrid[i, j].Symbol);
                }
                sb.AppendLine();
            }
            Console.Write(sb.ToString());
        }

        public bool IsWalkable(int x, int y) =>
            FieldGrid[x, y].Symbol == FIELD_BASE || FieldGrid[x, y].Symbol == Reward.RewardSign;
    }
}

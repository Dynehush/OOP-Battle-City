using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace Battle_City
{
    class Field
    {
        public const int Width = 40;
        public const int Height = 40;
        public const string FieldBase = "  ";
        public const string FieldBarrier = "🧱";
        public const int NumberOfLevels = 3;
        private int _level { get; }

        public BaseObject[,] FieldGrid { get; } = new BaseObject[Width, Height];

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
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (i == 0 || i == Width - 1 || j == 0 || j == Height - 1)
                    {
                        FieldGrid[i, j] = new BarrierObject(i, j, FieldBarrier);
                    }
                    else if (Game.Random.NextDouble() < possibility)
                    {
                        FieldGrid[i, j] = new BarrierObject(i, j, FieldBarrier);
                    }
                    else
                    {
                        FieldGrid[i, j] = new EmptyObject(i, j, FieldBase);
                    }
                }
            }
        }

        public void DisplayField()
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    sb.Append(FieldGrid[i, j].Symbol);
                }
                sb.AppendLine();
            }
            Console.Write(sb.ToString());
        }

        public bool IsWalkable(int x, int y) =>
            FieldGrid[x, y].Symbol == FieldBase || FieldGrid[x, y].Symbol == Reward._rewardSign;
    }
}

using System.Text;

namespace Battle_City
{
    class Field
    {
        public const int Width = 30;
        public const int Height = 30;
        public const string FieldBase = " ";
        public const string FieldBarrier = "#";
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

        // 🆕 Метод для малювання в буфер
        public void DrawToBuffer(char[,] buffer)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    string symbol = FieldGrid[x, y].Symbol;

                    int bufX = x;
                    int bufY = y;

                    if (bufX >= 0 && bufX < buffer.GetLength(0) && bufY >= 0 && bufY < buffer.GetLength(1))
                    {
                        buffer[bufX, bufY] = symbol[0];
                        if (symbol.Length > 1 && bufX < buffer.GetLength(0))
                            buffer[bufX, bufY] = symbol[1];
                    }
                }
            }
        }

        public bool IsWalkable(int x, int y) =>
            FieldGrid[x, y].Symbol == FieldBase || FieldGrid[x, y].Symbol == Reward._rewardSign;
    }
}


using Battle_City.Core.Services;
using Battle_City.Rendering;

namespace Battle_City.Core.Map
{
    public class Field
    {
        public const int Width = 30;
        public const int Height = 30;
        public const int NumberOfLevels = 3;
        private int _level { get; }
        private BaseObject[,] _fieldGrid { get; } = new BaseObject[Width, Height];
        public static Queue<(int x, int y)> WalkableCells = new Queue<(int x, int y)>();

        public Field(int level)
        {
            _level = level;

            bool ok;
            HashSet<(int, int)> reachable;
            do
            {
                GenerateField();
                (ok, reachable) = CheckConnectivity();
            } while (!ok);

            ReachableCells = reachable; 
        }
        public HashSet<(int, int)> ReachableCells { get; private set; }


        public BaseObject this[int x, int y]
        {
            get => _fieldGrid[x, y];
            set => _fieldGrid[x, y] = value;  
        }
        public void PlaceObject(int x, int y, BaseObject obj)
        {
            _fieldGrid[x, y] = obj;
        }
        public void RemoveObject(int x, int y)
        {
            _fieldGrid[x, y] = new EmptyObject(x, y);
        }
        public IEnumerable<BaseObject> GetAllObjects()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    yield return _fieldGrid[x, y];
        }

        public (bool, HashSet<(int, int)>) CheckConnectivity()
        {
            bool[,] visited = new bool[Width, Height];
            Queue<(int, int)> q = new();
            HashSet<(int, int)> reachable = new();

            bool foundStart = false;
            for (int x = 0; x < Width && !foundStart; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (_fieldGrid[x, y].IsWalkable())
                    {
                        q.Enqueue((x, y));
                        visited[x, y] = true;
                        reachable.Add((x, y));
                        foundStart = true;
                        break;
                    }
                }
            }

            if (!foundStart)
                return (true, reachable);

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            while (q.Count > 0)
            {
                var (cx, cy) = q.Dequeue();
                for (int k = 0; k < 4; k++)
                {
                    int nx = cx + dx[k], ny = cy + dy[k];
                    if (nx >= 0 && ny >= 0 && nx < Width && ny < Height &&
                        _fieldGrid[nx, ny].IsWalkable() && !visited[nx, ny])
                    {
                        visited[nx, ny] = true;
                        reachable.Add((nx, ny));
                        q.Enqueue((nx, ny));
                    }
                }
            }

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    if (_fieldGrid[x, y].IsWalkable() && !visited[x, y])
                        return (false, reachable);

            return (true, reachable);
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
                        _fieldGrid[i, j] = new BarrierObject(i, j);
                    }
                    else if (Game.Random.NextDouble() < possibility)
                    {
                        _fieldGrid[i, j] = new BarrierObject(i, j);
                    }
                    else
                    {
                        _fieldGrid[i, j] = new EmptyObject(i, j);
                    }
                }
            }
        }
    }
}

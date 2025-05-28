using Battle_City.Core.GameElements;
using System.Numerics;

namespace Battle_City.Core.Actor
{
    public class Enemy : Entity
    {
        public Direction? EnemyDirection { get; set; } = Direction.Right;
        public Enemy(Field _field) : base(0, 0)
        {
            var spawnPos = _field.ReachableCells
                        .OrderByDescending(p => p.Item2)
                        .ThenByDescending(p => p.Item1)
                        .FirstOrDefault();

            if (spawnPos == default)
                throw new InvalidOperationException("There is no available position to set a player on.");

            X = spawnPos.Item1;
            Y = spawnPos.Item2;
        }
        public void Move(Field _field, Player _player)
        {
            int oldX = X;
            int oldY = Y;
            var path = FindPath(_field, X, Y, _player.X, _player.Y);
            if (path != null && path.Count > 1)
            {
                var nextStep = path[1];
                X = nextStep.Item1;
                Y = nextStep.Item2;
            }
            int dx = oldX - X;
            int dy = oldY - Y;

            foreach (var direction in DirectionOffsets)
            {
                if (direction.Value == (dx, dy))
                {
                    EnemyDirection = direction.Key;
                    break;
                }
            }
        }

        private List<Tuple<int, int>> FindPath(Field field, int startX, int startY, int goalX, int goalY)
        {
            var openList = new List<Tuple<int, int>>();
            var closedList = new HashSet<Tuple<int, int>>();
            var gScore = new Dictionary<Tuple<int, int>, int>();
            var fScore = new Dictionary<Tuple<int, int>, int>();
            var cameFrom = new Dictionary<Tuple<int, int>, Tuple<int, int>>();

            var start = Tuple.Create(startX, startY);
            var goal = Tuple.Create(goalX, goalY);

            openList.Add(start);
            gScore[start] = 0;
            fScore[start] = Heuristic(start, goal);

            while (openList.Count > 0)
            {
                var current = openList.OrderBy(node => fScore.ContainsKey(node) ? fScore[node] : int.MaxValue).First(); 

                if (current.Equals(goal))
                {
                    return ReconstructPath(cameFrom, current);
                }

                openList.Remove(current);
                closedList.Add(current);

                foreach (var neighbor in GetNeighbors(field, current))
                {
                    if (closedList.Contains(neighbor))
                    {
                        continue;
                    }

                    var tentativeGScore = gScore[current] + 1;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                    else if (tentativeGScore >= gScore[neighbor])
                    {
                        continue;
                    }

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, goal);
                }
            }

            return null;
        }

        private int Heuristic(Tuple<int, int> a, Tuple<int, int> b)
        {
            return Math.Abs(a.Item1 - b.Item1) + Math.Abs(a.Item2 - b.Item2);
        }

        private List<Tuple<int, int>> GetNeighbors(Field field, Tuple<int, int> node)
        {
            var neighbors = new List<Tuple<int, int>>();
            var directions = new List<Tuple<int, int>>
            {
                Tuple.Create(0, 1),
                Tuple.Create(1, 0),
                Tuple.Create(0, -1),
                Tuple.Create(-1, 0)
            };

            foreach (var direction in directions)
            {
                var newX = node.Item1 + direction.Item1;
                var newY = node.Item2 + direction.Item2;

                if (!IsOutOfBounds(field) && field[newX, newY].IsWalkable()
                    && field[newX, newY].GetType() != typeof(Reward))
                {
                    neighbors.Add(Tuple.Create(newX, newY));
                }
            }

            return neighbors;
        }

        private List<Tuple<int, int>> ReconstructPath(Dictionary<Tuple<int, int>, Tuple<int, int>> cameFrom, Tuple<int, int> current)
        {
            var totalPath = new List<Tuple<int, int>> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Insert(0, current);
            }
            return totalPath;
        }

        public void Shoot(Entity entity)
        {
            int possibility = Game.Random.Next(0, 2);
            if (possibility == 1)
            {
                base.Shoot(entity);
            } else {return;}
        }
        public void Respawn(Field _field)
        {
            do
            {
                GetRandomCoordinates();
            } while (_field[X, Y].IsWalkable());
        }
        public void OnHit(Field _field)
        {
            Respawn(_field);
        }
    }
}

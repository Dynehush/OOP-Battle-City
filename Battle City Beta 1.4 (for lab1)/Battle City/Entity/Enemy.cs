using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Battle_City
{
    class Enemy : BaseObject
    {
        private static int _x = Field.Width - 4;
        private static int _y = Field.Height - 4;
        internal static readonly string _enemyFigure = "❯";
        public static readonly string EnemyBullet = "•";

        public List<Bullet> Bullets { get; } = new List<Bullet>();

        public new int X
        {
            get => _x;
            set
            {
                if (value >= 0 && value < Field.Width)
                {
                    _x = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Enemy coordinates are out of the field");
                }
            }
        }

        public new int Y
        {
            get => _y;
            set
            {
                if (value >= 0 && value < Field.Height)
                {
                    _y = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Enemy coordinates are out of the field");
                }
            }
        }

        public Enemy(Field _field) : base(_x, _y, _enemyFigure)
        {
            while (!_field.IsWalkable(X, Y))
            {
                X--;
                if (X <= 0)
                {
                    X = Field.Width - 2;
                    Y--;
                }
            }
        }

        public void DrawToBuffer(char[,] buffer)
        {
            int bufferX = X;
            int bufferY = Y;
            if (bufferX >= 0 && bufferX < buffer.GetLength(0) && bufferY >= 0 && bufferY < buffer.GetLength(1))
            {
                buffer[bufferX, bufferY] = _enemyFigure[0];
                if (_enemyFigure.Length > 1 && bufferX < buffer.GetLength(0))
                    buffer[bufferX, bufferY] = _enemyFigure[1];
            }
        }

        public override void Display() { Console.Write(_enemyFigure); }


        public int OldX { get; set; }
        public int OldY { get; set; }
        public void Move(Field _field, Player _player)
        {
            OldX = X;
            OldY = Y;

            int possibility = Game.Random.Next(0, 2);
            if (possibility == 1)
            {
                return;
            }
            var path = FindPath(_field, X, Y, _player.X, _player.Y);
            if (path != null && path.Count > 1)
            {
                var nextStep = path[1];
                X = nextStep.Item1;
                Y = nextStep.Item2;
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

                if (newX >= 0 && newX < Field.Width && newY >= 0 && newY < Field.Height && field.IsWalkable(newX, newY) 
                    && field.FieldGrid[newX, newY].Symbol != Reward._rewardSign)
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

        public void Shoot(Field _field, Player _player, Enemy _enemy)
        {
            int possibility = Game.Random.Next(0, 5);
            if (possibility == 1)
            {
                if (_player.X < _enemy.X && _player.Y < _enemy.Y)
                {
                    Bullets.Add(new Bullet(X, Y, -1, -1, EnemyBullet));
                }
                else if (_player.X > _enemy.X && _player.Y < _enemy.Y)
                {
                    Bullets.Add(new Bullet(X, Y, 1, -1, EnemyBullet));
                }
                else if (_player.X < _enemy.X && _player.Y > _enemy.Y)
                {
                    Bullets.Add(new Bullet(X, Y, -1, 1, EnemyBullet));
                }
                else if (_player.X > _enemy.X && _player.Y > _enemy.Y)
                {
                    Bullets.Add(new Bullet(X, Y, 1, 1, EnemyBullet));
                }
                else if (_player.X < _enemy.X && _player.Y == _enemy.Y)
                {
                    Bullets.Add(new Bullet(X, Y, -1, 0, EnemyBullet));
                }
                else if (_player.X > _enemy.X && _player.Y == _enemy.Y)
                {
                    Bullets.Add(new Bullet(X, Y, 1, 0, EnemyBullet));
                }
                else if (_player.X == _enemy.X && _player.Y > _enemy.Y)
                {
                    Bullets.Add(new Bullet(X, Y, 0, 1, EnemyBullet));
                }
                else if (_player.X == _enemy.X && _player.Y < _enemy.Y)
                {
                    Bullets.Add(new Bullet(X, Y, 0, -1, EnemyBullet));
                }
            } else {return;}
        }
        public void IsOverlapping(Enemy _enemy)
        {
            var bulletsToRemove = new List<Bullet>();

            foreach (Bullet bullet in Bullets)
            {
                if (_enemy.X == bullet.X && _enemy.Y == bullet.Y)
                {
                    bulletsToRemove.Add(bullet);
                }
            }

            foreach (Bullet bullet in bulletsToRemove)
            {
                Bullets.Remove(bullet);
            }
        }
    }
}

using Battle_City.Core.Map;
using static Battle_City.Core.Entities.Actor;
using Battle_City.Core.Services;

namespace Battle_City.Core.Entities
{
    class Reward : BaseObject
    {
        public const int NumberOfRewards = 5;
        internal static readonly HashSet<(int, int)> _rewardPositions = new HashSet<(int, int)>();

        public Reward(int x, int y) : base(x, y)
        {
            X = x;
            Y = y;
        }
        public override bool IsWalkable() => true;
        public static void SetRewards(int score, Field _field, Player _player)
        {
            RemoveRewards(_field);

            for (int i = 1; i <= NumberOfRewards - score; i++)
            {
                int x, y;
                do
                {
                    x = Game.Random.Next(Field.Width / 5, Field.Width);
                    y = Game.Random.Next(Field.Height / 5, Field.Height);
                }
                while (!_field[x, y].IsWalkable()
                       || _rewardPositions.Contains((x, y))
                       || _player.IsAtPosition(x, y)
                       || Interaction.IsNearEntity(x, y, _player));

                _field.PlaceObject(x, y, new Reward(x, y));
                _rewardPositions.Add((x, y));
            }
        }

        public static bool IsReward(int x, int y) { return _rewardPositions.Contains((x, y)); }

        public static int GetReward(int x, int y, Field _field)
        {
            if (IsReward(x, y))
            {
                _field.RemoveObject(x, y);
                _rewardPositions.Remove((x, y));
                return 1;
            }
            return 0;
        }

        public static void CheckRewards(int score, Field _field)
        {
            int counter = 0;
            for (int x = 0; x < Field.Width; x++)
            {
                for (int y = 0; y < Field.Height; y++)
                {
                    if (_field[x, y].GetType() == typeof(Reward))
                    {
                        counter++;
                    }
                }
            }

            if (counter > NumberOfRewards - score)
            {
                counter = counter - NumberOfRewards - score;
                for (int i = 1; i <= counter; i++)
                {
                    RemoveRewards(_field);
                }
            }
        }

        private static void RemoveRewards(Field _field)
        {
            foreach (var pos in _rewardPositions)
            {
                _field.RemoveObject(pos.Item1, pos.Item2);
            }
            _rewardPositions.Clear();
        }
    }
}

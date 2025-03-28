using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SomeWeirdGame
{
    class Reward : BaseObject
    {
        public static readonly string RewardSign = "⭐"; // Star
        public int NumberOfRewards { get; }
        private readonly HashSet<(int, int)> _rewardPositions = new HashSet<(int, int)>();

        public Reward(int number) : base(0, 0, RewardSign)
        {
            NumberOfRewards = number;
        }

        public void SetRewards(int score, Field _field, Player _player)
        {
            RemoveRewards(_field);

            for (int i = 1; i <= NumberOfRewards - score; i++)
            {
                int x, y;
                do
                {
                    x = Game.Random.Next(Field.WIDTH / 5, Field.WIDTH);
                    y = Game.Random.Next(Field.HEIGHT / 5, Field.HEIGHT);
                } while (!_field.IsWalkable(x, y) || _rewardPositions.Contains((x, y)) || (_player.X == x && _player.Y == y) || IsNearPlayer(x, y, _player));

                _field.FieldGrid[x, y] = new BaseObject(x, y, RewardSign);
                _rewardPositions.Add((x, y));
            }
        }

        private bool IsNearPlayer(int x, int y, Player _player)
        {
            int dx = Math.Abs(_player.X - x);
            int dy = Math.Abs(_player.Y - y);

            return dx <= 2 && dy <= 2;
        }

        public bool IsReward(int x, int y) { return _rewardPositions.Contains((x, y)); }

        public int GetReward(int x, int y, Field _field)
        {
            if (IsReward(x, y))
            {
                _field.FieldGrid[x, y] = new EmptyObject(x, y, Field.FIELD_BASE);
                _rewardPositions.Remove((x, y));
                return 1;
            }
            return 0;
        }

        public void CheckRewards(int score, Field _field)
        {
            int counter = 0;
            for (int x = 0; x < Field.WIDTH; x++)
            {
                for (int y = 0; y < Field.HEIGHT; y++)
                {
                    if (_field.FieldGrid[x, y].Symbol == RewardSign)
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

        private void RemoveRewards(Field _field)
        {
            for (int x = 0; x < Field.WIDTH; x++)
            {
                for (int y = 0; y < Field.HEIGHT; y++)
                {
                    if (_field.FieldGrid[x, y].Symbol == RewardSign)
                    {
                        _field.FieldGrid[x, y] = new EmptyObject(x, y, Field.FIELD_BASE);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Battle_City
{
    class Reward : BaseObject
    {
        public static readonly string _rewardSign = "@"; 
        public int NumberOfRewards { get; }
        internal readonly HashSet<(int, int)> _rewardPositions = new HashSet<(int, int)>();

        public Reward(int number) : base(0, 0, _rewardSign)
        {
            NumberOfRewards = number;
        }

        public void SetRewards(int score, Field _field, Player _player)
        {
            if (_rewardPositions.Count > 0) return;

            RemoveRewards(_field);

            for (int i = 1; i <= NumberOfRewards - score; i++)
            {
                int x, y;
                do
                {
                    x = Game.Random.Next(Field.Width / 5, Field.Width);
                    y = Game.Random.Next(Field.Height / 5, Field.Height);
                } while (!_field.IsWalkable(x, y) || _rewardPositions.Contains((x, y)) 
                || (_player.X == x && _player.Y == y) 
                || IsNearPlayer(x, y, _player));

                _field.FieldGrid[x, y] = new BaseObject(x, y, _rewardSign);
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
                _field.FieldGrid[x, y] = new EmptyObject(x, y, Field.FieldBase);
                _rewardPositions.Remove((x, y));
                return 1;
            }
            return 0;
        }

        public void CheckRewards(int score, Field _field)
        {
            int counter = 0;
            for (int x = 0; x < Field.Width; x++)
            {
                for (int y = 0; y < Field.Height; y++)
                {
                    if (_field.FieldGrid[x, y].Symbol == _rewardSign)
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
            for (int x = 0; x < Field.Width; x++)
            {
                for (int y = 0; y < Field.Height; y++)
                {
                    if (_field.FieldGrid[x, y].Symbol == _rewardSign)
                    {
                        _field.FieldGrid[x, y] = new EmptyObject(x, y, Field.FieldBase);
                    }
                }
            }
        }
    }
}

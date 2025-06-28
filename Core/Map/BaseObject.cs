using Battle_City.Core.Services;

namespace Battle_City.Core.Map
{
    public abstract class BaseObject
    {
        private int _x;
        private int _y;
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
        public virtual bool IsWalkable() => false;
        public BaseObject(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool IsOutOfBounds(Field _field)
        {
            return X < 0 || X >= Field.Width || Y < 0 || Y >= Field.Height;
        }
        public void GetRandomCoordinates()
        {
            X = Game.Random.Next(2, Field.Width - 2);
            Y = Game.Random.Next(2, Field.Height - 2);
        }
    }
}

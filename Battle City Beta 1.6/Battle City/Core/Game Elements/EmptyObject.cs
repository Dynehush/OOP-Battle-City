namespace Battle_City.Core.GameElements
{
    class EmptyObject : BaseObject
    {
        public override bool IsWalkable() => true;
        public EmptyObject(int x, int y) : base(x, y)
        {
        }
    }
}

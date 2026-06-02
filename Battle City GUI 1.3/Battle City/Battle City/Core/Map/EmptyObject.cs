namespace Battle_City.Core.Map
{
    class EmptyObject : BaseObject
    {
        public override bool IsWalkable() => true;
        public EmptyObject(int x, int y) : base(x, y)
        {
        }
    }
}

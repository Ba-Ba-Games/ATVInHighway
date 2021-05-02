namespace Base.Game.GameObject
{
    using Base.Game.Signal;
    using Base.Util;
    public class PoolableObject : MyObject
    {
        public virtual ObjectType Type { get => ObjectType.TYPE1; }

        protected override void OnEnable()
        {
            base.OnEnable();
            SignalManager.Fire(typeof(PoolableObject), this);
        }

    }
}

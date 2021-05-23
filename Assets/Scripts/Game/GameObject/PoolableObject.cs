namespace Base.Game.GameObject
{
    using Base.Game.Signal;
    using Base.Util;
    public class PoolableObject : MyObject
    {
        public virtual ObjectType Type { get => ObjectType.TYPE1; }

        protected override void OnDisable()
        {
            base.OnDisable();
            SignalManager.Fire(typeof(PoolableObject), this);
        }

    }
}

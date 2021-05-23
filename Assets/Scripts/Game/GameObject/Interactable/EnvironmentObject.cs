namespace Base.Game.GameObject.Interactable
{
    public class EnvironmentObject : PoolableObject
    {
        protected override void OnDisable()
        {
            base.OnDisable();
            if (gameObject.activeSelf)
            {
                DeActivate();
            }
        }
    }
}

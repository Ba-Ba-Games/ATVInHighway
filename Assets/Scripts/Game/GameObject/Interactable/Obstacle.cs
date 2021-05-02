namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    public class Obstacle : MyObject
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if(collision.collider.GetComponentInParent<BaseVehicle>() is BaseVehicle vehicle)
            {
                vehicle.Crash(this);
            }
        }
    }
}

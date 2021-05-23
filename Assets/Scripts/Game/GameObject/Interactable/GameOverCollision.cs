namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using Base.Game.Signal;

    public class GameOverCollision : MyObject
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if(collision.collider.GetComponentInParent<BaseVehicle>() is BaseVehicle vehicle)
            {
                SignalManager.Fire(typeof(SignalGameOver), vehicle is PlayerVehicle);
            }
        }

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            if (other.GetComponentInParent<BaseVehicle>() is BaseVehicle vehicle)
            {
                SignalManager.Fire(typeof(SignalSetLevel));
            }
        }

    }
}

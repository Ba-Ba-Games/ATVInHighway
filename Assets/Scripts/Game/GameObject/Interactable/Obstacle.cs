namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using UnityEngine;

    public class Obstacle : PoolableObject
    {
        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if(collision.collider.GetComponentInParent<BaseVehicle>() is BaseVehicle vehicle)
            {
                vehicle.Crash(this);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (gameObject.activeSelf)
            {
                DeActivate();
            }
        }

        public void PutPlatform(BasePlatform platform, Vector3 pos)
        {
            transform.SetParent(platform.transform);
            transform.localPosition = pos;
            Activate();
        }

    }
}

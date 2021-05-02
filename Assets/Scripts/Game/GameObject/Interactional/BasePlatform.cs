namespace Base.Game.GameObject.Interactional
{
    using Base.Game.GameObject.Interactable;
    using Base.Util;
    using UnityEngine;
    public class BasePlatform : PoolableObject
    {
        [SerializeField] private ObjectType _type = ObjectType.TYPE1;
        public override ObjectType Type => _type;
        private EndPoint _endPoint;
        public Vector3 EndPoint { get => _endPoint.transform.position; }
        protected override void Initialize()
        {
            base.Initialize();
            if(GetComponentInChildren<EndPoint>() is EndPoint endPoint)
            {
                _endPoint = endPoint;
            }
            else
            {
                Debug.LogWarning("Please check it endpoint of platform type " + _type);
            }
        }

        public void SetPlatform(BasePlatform before = null)
        {
            transform.position = before ? before.EndPoint : Vector3.zero;
        }

        public void PutVehicle(BaseVehicle vehicle)
        {
            vehicle.transform.position = transform.position + new Vector3(0, 2, 2);
        }

    }
}

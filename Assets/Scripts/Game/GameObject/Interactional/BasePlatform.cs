namespace Base.Game.GameObject.Interactional
{
    using Base.Game.GameObject.Interactable;
    using Base.Util;
    using System.Collections.Generic;
    using UnityEngine;
    public class BasePlatform : PoolableObject
    {
        [SerializeField] private ObjectType _type = ObjectType.TYPE1;
        [SerializeField] private float _leftLine = -1.5f;
        [SerializeField] private float _rightLine = 1.5f;

        public override ObjectType Type => _type;
        private EndPoint _endPoint;
        public Vector3 EndPoint { get => _endPoint.transform.position; }

        private List<EnvironmentObject> _environmentObjs = new List<EnvironmentObject>();
        private List<Obstacle> _obstacles = new List<Obstacle>();

        protected override void Initialize()
        {
            base.Initialize();
            if (GetComponentInChildren<EndPoint>() is EndPoint endPoint)
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

        protected override void OnEnable()
        {
            base.OnEnable();
            SetObstacle();
            SetEnvironment();
        }

        private void SetEnvironment()
        {
            List<Vector3> usedPositions = new List<Vector3>();
            for (int i = 0; i < 4; i++)
            {
                Vector3 pos = new Vector3(Random.Range(1, 4) * 5, 0, Random.Range(1, 8) * 5);
                while (usedPositions.Contains(pos))
                {
                    pos = new Vector3(Random.Range(1, 4) * 5, 0, Random.Range(1, 8) * 5);
                }
                usedPositions.Add(pos);
                EnvironmentObject obj = Factory.Instance.GetObject(typeof(EnvironmentObject)) as EnvironmentObject;
                obj.transform.SetParent(transform);
                obj.transform.localPosition = pos;
                obj.Activate();
            }
        }

        private void SetObstacle()
        {
            int obstacleCounts = Random.Range(0, 3);
            List<Vector3> usedTransforms = new List<Vector3>();
            for (int i = 0; i < obstacleCounts; i++)
            {
                float x = Random.Range(0, 2) == 0 ? _rightLine : _leftLine;
                float z = Random.Range(1, 4) * 10;
                Vector3 t = new Vector3(x, transform.position.y, z);
                while (usedTransforms.Contains(t))
                {
                    x = Random.Range(0, 2) == 0 ? _rightLine : _leftLine;
                    z = Random.Range(1, 4) * 10;
                    t = new Vector3(x, transform.position.y, z);
                }
                usedTransforms.Add(t);
                if (Random.Range(0, 5) == 0)
                {
                    NPCVehicle vehicle = Factory.Instance.GetObject(typeof(NPCVehicle)) as NPCVehicle;
                    vehicle.PutPlatform(this, t, t.x == _rightLine);
                }
                else
                {
                    Obstacle obstacle = (Factory.Instance.GetObject(typeof(Obstacle)) as Obstacle);
                    obstacle.PutPlatform(this, t);
                }
            }
        }

    }
}

namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using System;
    using System.Collections;
    using UnityEngine;

    public class Chassis : MyObject
    {
        [SerializeField] private float _movementSpeed = 1f;
        [SerializeField] private float _upLimit = 2f;
        private Coroutine _chassisUpRoutine;
        private Vector3 _defaultLocalPos;

        private BaseVehicle _vehicle;
        public event Action<bool> ChassisUpped;
        public event Action<Vector3> PositionChanged;

        protected override void Initialize()
        {
            _defaultLocalPos = transform.localPosition;
            _vehicle = GetComponentInParent<BaseVehicle>();
            base.Initialize();
        }

        protected override void Registration()
        {
            base.Registration();
            _vehicle.ChassisUpped += OnChassisUpped;
        }

        protected override void UnRegistration()
        {
            base.UnRegistration();
            _vehicle.ChassisUpped -= OnChassisUpped;
        }

        private void OnChassisUpped(bool statu)
        {
            if(_chassisUpRoutine != null)
            {
                StopCoroutine(_chassisUpRoutine);
            }
            _chassisUpRoutine = StartCoroutine(ChassisAction(statu));
        }

        private IEnumerator ChassisAction(bool statu)
        {
            ChassisUpped?.Invoke(statu);
            float targetY = statu ? _upLimit : _defaultLocalPos.y;
            Vector3 target = new Vector3(transform.localPosition.x, targetY, transform.localPosition.z);
            while (!transform.localPosition.Equals(target))
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, _movementSpeed * Time.deltaTime);
                PositionChanged?.Invoke(transform.position);
                yield return null;
            }
            _chassisUpRoutine = null;
        }

    }
}

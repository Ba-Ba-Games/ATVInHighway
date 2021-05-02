namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using System;
    using UnityEngine;
    public class Wheel : MyObject
    {
        [SerializeField] private float _horizontalMovementSpeed = 2f;
        [SerializeField] private bool _isRightWheel = false;
        [SerializeField] private WheelCollider _ownCollider = null;
        private Chassis _chassis;
        private BaseVehicle _vehicle;
        private bool _chassisStatu;
        public event Action<Vector3> PositionChanged;

        private Vector3 _defaultLocalPos;
        private Vector3 Position { get => _ownCollider.transform.localPosition; set => _ownCollider.transform.localPosition = value; }

        protected override void Initialize()
        {
            _vehicle = GetComponentInParent<BaseVehicle>();
            _chassis = _vehicle.GetComponentInChildren<Chassis>();
            _defaultLocalPos = _ownCollider.transform.localPosition;
            base.Initialize();
        }

        protected override void Registration()
        {
            base.Registration();
            _vehicle.PositionChanged += OnVehiclePositionChanged;
            _chassis.PositionChanged += OnChassisPositionChanged;
            _chassis.ChassisUpped += OnChassisUpped;
        }

        protected override void UnRegistration()
        {
            base.UnRegistration();
            _vehicle.PositionChanged -= OnVehiclePositionChanged;
            _chassis.PositionChanged -= OnChassisPositionChanged;
            _chassis.ChassisUpped -= OnChassisUpped;
        }

        private void OnVehiclePositionChanged(Vector3 obj)
        {
            PositionChanged?.Invoke(transform.position);
        }

        private void OnChassisUpped(bool statu)
        {
            _chassisStatu = statu;
        }

        private void OnChassisPositionChanged(Vector3 obj)
        {
            Vector3 target = _chassisStatu ? new Vector3(_defaultLocalPos.x + (_isRightWheel ? 1f : -1f), Position.y, Position.z) : _defaultLocalPos;
            Position = Vector3.MoveTowards(Position, target, _horizontalMovementSpeed * Time.deltaTime);
        }
    }
}

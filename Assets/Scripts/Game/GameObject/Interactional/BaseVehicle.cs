namespace Base.Game.GameObject.Interactional
{
    using Base.Game.GameObject.Interactable;
    using Base.Game.Signal;
    using System;
    using UnityEngine;
    using UnityStandardAssets.Vehicles.Car;

    [RequireComponent(typeof(CarController), typeof(Rigidbody))]
    public class BaseVehicle : PoolableObject
    {
        [Range(0f,1f)] [SerializeField] private float _defaultAcceleration = 1f;
        protected Rigidbody _body;
        protected CarController _controller;
        protected float _acceleration;
        protected float _steering;
        protected bool _canMove;
        public float Speed { get => _controller.CurrentSpeed; }

        public event Action<Obstacle> Crashed;
        public event Action<bool> ChassisUpped;
        public event Action<Vector3> PositionChanged;

        protected override void Initialize()
        {
            base.Initialize();
            _body = GetComponent<Rigidbody>();
            _controller = GetComponent<CarController>();
            _acceleration = _defaultAcceleration;
            _canMove = true;
        }

        protected override void Registration()
        {
            base.Registration();
            SignalManager.Register(this);
        }

        protected override void UnRegistration()
        {
            base.UnRegistration();
            SignalManager.UnRegister(this);
        }

        public override void Activate()
        {
            base.Activate();
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Registration();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnRegistration();
        }

        protected virtual void Move()
        {
            _controller.Move(_steering, _acceleration, _acceleration, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0), Time.fixedDeltaTime * Mathf.Clamp(Speed * 2f, 50f, 100f));
        }

        protected virtual void Steering(float steering)
        {
            _steering = steering;
        }

        protected virtual void Break(bool statu)
        {
            _acceleration = statu ? _defaultAcceleration * -1 : _defaultAcceleration;
        }

        public virtual void ChassisUp(bool statu)
        {
            ChassisUpped?.Invoke(statu);
        }

        protected virtual void FixedUpdate()
        {
            if (_canMove)
                Move();
            if (Speed != 0)
                PositionChanged?.Invoke(transform.position);
        }

        [Signal(typeof(SignalStartGame))]
        public void OnStartGame()
        {
            _canMove = true;
        }

        [Signal(typeof(SignalGameOver), typeof(bool))]
        public void OnGameOver(bool statu)
        {
            //_canMove = false;
        }

        public void Crash(Obstacle obstacle)
        {
            if (!_canMove)
                return;
            Crashed?.Invoke(obstacle);
            _canMove = false;
            _body.AddExplosionForce(Mathf.Clamp(Speed * 1000f, 10000f, 80000f), transform.position + new Vector3(0, -1, 1), 30f);
            if (this is PlayerVehicle)
                SignalManager.Fire(typeof(SignalCrashed));
        }

    }
}

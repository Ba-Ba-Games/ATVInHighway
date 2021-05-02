namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using Base.Game.Signal;
    using UnityEngine;
    [RequireComponent(typeof(Rigidbody))]
    public class Driver : MyObject
    {
        private Rigidbody _body;
        private BaseVehicle _vehicle;
        private Vector3 _defaultLocalPos;
        private Vector3 _defaultLocalRot;

        protected override void Initialize()
        {
            _defaultLocalPos = transform.localPosition;
            _defaultLocalRot = transform.localRotation.eulerAngles;
            _body = GetComponent<Rigidbody>();
            _vehicle = GetComponentInParent<BaseVehicle>();
            base.Initialize();
        }

        protected override void OnEnable()
        {
            _body.isKinematic = true;
            transform.localPosition = _defaultLocalPos;
            transform.localRotation = Quaternion.Euler(_defaultLocalRot);
            base.OnEnable();
        }

        protected override void Registration()
        {
            base.Registration();
            SignalManager.Register(this);
            _vehicle.Crashed += OnCrashed;
        }

        protected override void UnRegistration()
        {
            base.UnRegistration();
            SignalManager.UnRegister(this);
            _vehicle.Crashed -= OnCrashed;
        }

        private void OnCrashed(Obstacle obstacle)
        {
            _body.isKinematic = false;
            _body.AddExplosionForce(Mathf.Clamp(100f * _vehicle.Speed, 300f, 1500f), transform.position + new Vector3(0, -1, -1), 30f);
        }

        [Signal(typeof(SignalStartGame))]
        public void OnStartGame()
        {
            OnEnable();
        }
    }
}

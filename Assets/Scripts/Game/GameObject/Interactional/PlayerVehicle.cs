namespace Base.Game.GameObject.Interactional
{
    using Base.Game.Signal;
    using UnityEngine;
    public class PlayerVehicle : BaseVehicle
    {
        [SerializeField] private float _stabilizierMultipier = 1.25f;
        private float _leftLine = -1.1f;
        private float _rightLine = 1.1f;
        private bool _isRight;

        private Vector3 _target;

        [Signal(typeof(SignalJoystickMultipiers), typeof(float), typeof(float))]
        public void OnJoystickMultipiers(float h, float v)
        {
            if(h > .5f)
            {
                _isRight = true;
            }else if(h < -.5f)
            {
                _isRight = false;
            }
            //Steering(h);
            ChassisUp(v >= .5f);
            Break(v <= -.5f);
        }

        private void Update()
        {
            _target = new Vector3(_isRight ? _rightLine : _leftLine, transform.position.y, transform.position.z + 1f);
            _steering = (_target - transform.position).normalized.x;
            _target.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, _target, ((_controller.CurrentSpeed/_controller.MaxSpeed) * _stabilizierMultipier)*Time.deltaTime);
        }

        [Signal(typeof(SignalStartGame))]
        public void OnStartGame()
        {
            _canMove = true;
        }

        [Signal(typeof(SignalGameOver), typeof(bool))]
        public void OnGameOver(bool statu)
        {
            _canMove = false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            transform.position = new Vector3(1.1f, 1, 15f);
            _target = new Vector3(_rightLine, transform.position.y, transform.position.z);
            _isRight = true;
            _canMove = false;
        }

    }
}
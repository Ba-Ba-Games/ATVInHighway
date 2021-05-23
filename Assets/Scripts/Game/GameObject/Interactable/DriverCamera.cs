namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using System;
    using UnityEngine;
    using Cinemachine;
    using System.Collections;
    using Base.Game.Signal;

    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class DriverCamera : MyObject
    {
        [SerializeField] private float _amplitudePower = 10f;
        [Range(0f, 3f)] [SerializeField] private float _actionTime = 2f;
        private float _defaultAmplitude;
        private BaseVehicle _vehicle;
        private CinemachineVirtualCamera _vCam;
        private CinemachineBasicMultiChannelPerlin _perlin;

        private Coroutine _perlinRoutine;

        protected override void Initialize()
        {
            _vehicle = GetComponentInParent<BaseVehicle>();
            _vCam = GetComponent<CinemachineVirtualCamera>();
            _perlin = _vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _defaultAmplitude = _perlin.m_AmplitudeGain;
            base.Initialize();
        }

        protected override void Registration()
        {
            base.Registration();
            _vehicle.Crashed += OnCrashed;
            SignalManager.Register(this);
        }

        protected override void UnRegistration()
        {
            base.UnRegistration();
            _vehicle.Crashed -= OnCrashed;
            SignalManager.UnRegister(this);
        }

        private void OnCrashed(Obstacle obj)
        {
            if (_perlinRoutine != null)
                StopCoroutine(_perlinRoutine);
            _perlinRoutine = StartCoroutine(PerlinAction());
        }

        private IEnumerator PerlinAction()
        {
            _perlin.m_AmplitudeGain = _amplitudePower;
            yield return new WaitForSeconds(_actionTime);
            _perlin.m_AmplitudeGain = _defaultAmplitude;
            SignalManager.Fire(typeof(SignalSetLevel));
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _perlin.m_AmplitudeGain = _defaultAmplitude;
        }

        [Signal(typeof(SignalGameOver), typeof(bool))]
        public void OnGameOver(bool statu)
        {
            _perlin.m_AmplitudeGain = _defaultAmplitude;
        }

    }
}

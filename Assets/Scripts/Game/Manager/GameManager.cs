namespace Base.Game.Manager
{
    using Base.Game.Factory;
    using Base.Game.GameObject;
    using Base.Game.GameObject.Interactable;
    using Base.Game.GameObject.Interactional;
    using Base.Game.Signal;
    using System;
    using System.Collections;
    using UnityEngine;
    public class GameManager : MonoBehaviour
    {
        private IFactory<PoolableObject> _generalFactory;
        private PlayerVehicle _player;
        private void Awake()
        {
            SignalManager.Register(this);
            _generalFactory = new Factory<PoolableObject>.Builder().AddAllPrefabOnPath("Vehicles").Register().Build();
            _player = _generalFactory.GetObject(typeof(PlayerVehicle)) as PlayerVehicle;
            _player.Crashed += OnCrashed;
        }

        private void OnCrashed(Obstacle obj)
        {
            StartCoroutine(CrashedAction());
        }

        private IEnumerator CrashedAction()
        {
            yield return new WaitForSeconds(5f);
            OnGameOver(false);
        }

        private void OnDestroy()
        {
            _player.Crashed -= OnCrashed;
            SignalManager.UnRegister(this);
        }

        [Signal(typeof(SignalGameOver), typeof(bool))]
        public void OnGameOver(bool statu)
        {
            _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _player.transform.position = Vector3.zero;
            _player.transform.rotation = Quaternion.Euler(Vector3.zero);
            SignalManager.Fire(typeof(SignalStartGame));
        }

    }
}

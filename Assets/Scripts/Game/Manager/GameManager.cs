namespace Base.Game.Manager
{
    using Base.Game.GameObject;
    using Base.Game.GameObject.Interactable;
    using Base.Game.GameObject.Interactional;
    using Base.Game.Signal;
    using Base.Util;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class GameManager : MonoBehaviour
    {
        private PlayerVehicle _player;
        private List<BasePlatform> _platforms = new List<BasePlatform>();
        private void Awake()
        {
            SignalManager.Register(this);
            Factory.Constraction.AddAllPrefabOnPath("Vehicles").AddAllPrefabOnPath("Platforms");
            Factory.Constraction.AddAllPrefabOnPath("Obstacles").AddAllPrefabOnPath("Environments").Register().Build();
           
        }

        private void Start()
        {
            Invoke("OnNewLevel", .5f);
        }

        private void OnDestroy()
        {
            SignalManager.UnRegister(this);
        }

        [Signal(typeof(SignalSetLevel))]
        public void OnNewLevel()
        {
            if (_player)
            {
                _player.DeActivate();
            }
            _platforms.ForEach(p => p.DeActivate());
            _platforms.Clear();
            BasePlatform current = Factory.Instance.GetObject(typeof(BasePlatform)) as BasePlatform;
            current.SetPlatform();
            current.Activate();
            _platforms.Add(current);
            for(int i = 0; i < 10; i++)
            {
                BasePlatform next = Factory.Instance.GetObject(typeof(BasePlatform)) as BasePlatform;
                next.SetPlatform(current);
                current = next;
                current.Activate();
                _platforms.Add(current);
            }
            _player = Factory.Instance.GetObject(typeof(PlayerVehicle)) as PlayerVehicle;
            _player.Activate();
        }

    }
}

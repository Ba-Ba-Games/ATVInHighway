namespace Base.Game.GameObject.Interactable
{
    using System;
    using UnityEngine;
    public class SuspensionDownPart : MyObject
    {
        [SerializeField] private Wheel _ownWheel = null;
        public event Action<Vector3> PositionChanged;

        protected override void Registration()
        {
            base.Registration();
            _ownWheel.PositionChanged += OnWheelPositionChanged;
        }

        protected override void UnRegistration()
        {
            base.UnRegistration();
            _ownWheel.PositionChanged -= OnWheelPositionChanged;
        }

        protected virtual void OnWheelPositionChanged(Vector3 obj)
        {
            transform.position = obj;
            PositionChanged?.Invoke(transform.GetChild(0).position);
        }
    }
}

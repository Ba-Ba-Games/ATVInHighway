namespace Base.Game.GameObject.Interactable
{
    using System;
    using UnityEngine;
    public class SuspensionMiddlePart : MyObject
    {
        [SerializeField] private SuspensionDownPart _downPart = null;
        [SerializeField] private SuspensionUpPart _upPart = null;

        protected override void Registration()
        {
            base.Registration();
            _downPart.PositionChanged += OnDownPartPositionChanged;
        }

        protected override void UnRegistration()
        {
            base.UnRegistration();
            _downPart.PositionChanged -= OnDownPartPositionChanged;
        }

        private void OnDownPartPositionChanged(Vector3 obj)
        {
            transform.up = (_upPart.transform.position - transform.position).normalized;
            transform.position = obj;
            transform.localScale = new Vector3(transform.localScale.x, Vector3.Distance(transform.position, _upPart.transform.position) * 2f, transform.localScale.z);
        }
    }
}

namespace Base.Game.UI
{
    using Base.Game.GameObject;
    using Base.Game.Signal;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    public class ImageCrashed : MyObject
    {
        [Range(0f,2f)] [SerializeField] private float _actionTime = 1f;
        private Image _img;
        protected override void Initialize()
        {
            base.Initialize();
            _img = GetComponent<Image>();
            DeActivate();
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

        [Signal(typeof(SignalCrashed))]
        public void OnPlayerCrashed()
        {
            Activate();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            StartCoroutine(CrashedAction());
        }

        private IEnumerator CrashedAction()
        {
            bool statu = false;
            float time = 0;
            while (time < _actionTime)
            {
                time += .1f;
                statu = !statu;
                _img.enabled = statu;
                yield return new WaitForSeconds(.1f);
            }
            _img.enabled = true;
            DeActivate();
        }

    }
}

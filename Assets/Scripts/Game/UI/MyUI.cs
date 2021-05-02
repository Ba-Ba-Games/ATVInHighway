namespace Base.Game.UI
{
    using Base.Game.Signal;
    using UnityEngine;

    public class MyUI : MonoBehaviour
    {
        private void Awake()
        {
            Initialize();
        }

        protected virtual void OnDestroy()
        {
        }

        protected virtual void Initialize()
        {

        }


        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }

    }
}

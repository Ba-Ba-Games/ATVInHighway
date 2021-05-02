namespace Base.Game.GameObject
{
    using Base.Game.Signal;
    using UnityEngine;
    public class MyObject : MonoBehaviour
    {
        public Transform Transform => transform;

        private void Awake()
        {
            Initialize();
        }

        protected virtual void OnDestroy()
        {
            UnRegistration();
        }

        protected virtual void Initialize()
        {
            Registration();
        }

        protected virtual void Registration()
        {

        }

        protected virtual void UnRegistration()
        {

        }

        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }

        public virtual void Activate()
        {
            gameObject?.SetActive(true);
        }
        public virtual void DeActivate()
        {
            gameObject?.SetActive(false);
        }

    }
}

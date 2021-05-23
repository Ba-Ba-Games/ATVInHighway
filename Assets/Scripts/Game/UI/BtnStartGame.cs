namespace Base.Game.UI
{
    using Base.Game.Signal;
    using UnityEngine.EventSystems;
    public class BtnStartGame : BaseButton
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            SignalManager.Fire(typeof(SignalStartGame));
            Deactivate();
        }

        protected override void Initialize()
        {
            base.Initialize();
            SignalManager.Register(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SignalManager.UnRegister(this);
        }

        [Signal(typeof(SignalSetLevel))]
        public void OnGameOver()
        {
            Activate();
        }

    }
}
namespace Base.Game.GameObject.Interactional
{
    using UnityEngine;
    public class NPCVehicle : BaseVehicle
    {
        protected override void OnDisable()
        {
            base.OnDisable();
            if (gameObject.activeSelf)
            {
                DeActivate();
            }
        }

        public void PutPlatform(BasePlatform platform, Vector3 pos , bool isRightLine)
        {
            transform.SetParent(platform.transform);
            transform.localPosition = pos;
            Activate();
            transform.rotation = Quaternion.Euler(isRightLine ? Vector3.zero : (Vector3.up * 180));
        }
    }
}

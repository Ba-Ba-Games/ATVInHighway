namespace Base.Game.GameObject.Interactional
{
    using Base.Game.Signal;
    using UnityEngine;
    public class PlayerVehicle : BaseVehicle
    {
        [Signal(typeof(SignalJoystickMultipiers), typeof(float), typeof(float))]
        public void OnJoystickMultipiers(float h, float v)
        {
            Steering(h);
            ChassisUp(v >= .5f);
            Break(v <= -.5f);
        }
    }
}
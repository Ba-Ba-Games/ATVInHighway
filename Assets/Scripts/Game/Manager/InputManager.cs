namespace Base.Game.Manager
{
    using Base.Game.Signal;
    using UnityEngine;

    public class InputManager : MonoBehaviour
    {
        private Joystick _joystick;

        private void Awake()
        {
            _joystick = FindObjectOfType<Joystick>();
        }

        private void Update()
        {
            if (_joystick)
                SignalManager.Fire(typeof(SignalJoystickMultipiers), _joystick.Horizontal, _joystick.Vertical);
            if (Input.GetKeyDown(KeyCode.Space))
                SignalManager.Fire(typeof(SignalSetLevel));
        }

    }
}

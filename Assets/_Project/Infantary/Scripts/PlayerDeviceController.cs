using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeviceController : MonoBehaviour
{
    [field: SerializeField] public Device device { get; set; }


    public void OnDeviceChange(PlayerInput playerInput)
    {
        device = playerInput.currentControlScheme.Equals("Gamepad") ? Device.Gamepad : Device.Keyboard;
    }
}

public enum Device
{
    Keyboard,
    Gamepad
}

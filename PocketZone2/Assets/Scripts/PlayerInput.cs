using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _movementJoystick;

    public Vector2 GetJoystickDirection()
    {
        return _movementJoystick.Direction;
    }

}

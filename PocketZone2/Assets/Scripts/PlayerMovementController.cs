using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speed;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        Vector3 direction = _playerInput.GetJoystickDirection();
        //print(direction);
        if (direction != Vector3.zero)
        {
            _transform.position += direction * _speed * Time.deltaTime;

            if (direction.x < 0)
                _transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                _transform.rotation = Quaternion.identity;
        }
    }
}

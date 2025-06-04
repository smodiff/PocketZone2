using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _objectToFollow;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position = Vector3.Lerp(_transform.position, _objectToFollow.position + _offset, _speed * Time.deltaTime);
    }
}

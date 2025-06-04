using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position += _speed * Time.deltaTime * -_transform.up;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<IDamagable>() != null)
        {
            collision.collider.GetComponent<IDamagable>().GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _shootGapTime;
    [SerializeField] private GunProjectile _projectilePrefab;
    [SerializeField] private Transform _projectileSpawn;

    public virtual void Shoot(Transform target)
    {
        GunProjectile bullet = Instantiate(_projectilePrefab, _projectileSpawn.position,
            Quaternion.identity);
        bullet.transform.up = -(target.position - bullet.transform.position);
        Destroy(bullet.gameObject, 10);
        Debug.DrawRay(transform.position, target.position - transform.position, Color.red, 10);
    }
}

using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _timeToAttack;
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected HealthBar _healthBar;
    [SerializeField] protected ItemScriptableObject[] _itemsToDrop;

    protected int _currentHealth;
    protected float _attackTimer;
    protected bool _isPlayerInSight = false;
    protected Transform _transform;

    protected virtual void Start()
    {
        
        _currentHealth = _maxHealth;
        _attackTimer = _timeToAttack;

        _healthBar.SetMaxHealth(_maxHealth);
        _healthBar.SetHealth(_currentHealth);

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _transform = transform;

        _transform.rotation = Quaternion.identity;
    }

    protected virtual void Update()
    {
        _attackTimer -= Time.deltaTime;

        if (_isPlayerInSight)
        {
            Move(GameController.instance.Player.transform.position);

            if (_agent.remainingDistance < _agent.stoppingDistance)
                Attack();

            if((GameController.instance.Player.transform.position - _transform.position).x < 0)
            {
                _transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                _transform.rotation = Quaternion.identity;
            }
        }

        
    }

    protected virtual void Move(Vector3 target)
    {
        _agent.SetDestination(target);
    }

    protected virtual void Attack()
    {
        if (_attackTimer <= 0)
        {
            GameController.instance.Player.GetDamage(_damage);
            _attackTimer = _timeToAttack;
        }
    }

    protected virtual void Death()
    {
        GameController.instance.SpawnItem(_itemsToDrop[Random.Range(0, _itemsToDrop.Length)], _transform.position);
        GameController.instance.EnemyDeath();
        Destroy(gameObject);
    }

    public virtual void GetDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);
        print("Hit! hp: " + _currentHealth);

        if(_currentHealth <= 0)
        {
            Death();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            _isPlayerInSight = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            _isPlayerInSight = false;
    }
}

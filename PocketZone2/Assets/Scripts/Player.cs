using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private PlayerMovementController _playerMovement;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private int _maxHealth;

    private int _currentHealth;
    private Transform _transform;
    private List<Enemy> _enemiesAround;

    private void Start()
    {
        _transform = transform;

        _currentHealth = _maxHealth; // !!!

        _enemiesAround = new List<Enemy>();

        _healthBar.SetMaxHealth(_maxHealth);
        _healthBar.SetHealth(_currentHealth);
    }

    public PlayerInventory GetInventory()
    {
        return _playerInventory;
    }
    
    public void Shoot()
    {
        if (_enemiesAround.Count == 0)
            return;

        Enemy nearestEnemy = _enemiesAround[0];

        foreach(Enemy enemy in _enemiesAround)
        {
            if((enemy.transform.position - _transform.position).sqrMagnitude < (nearestEnemy.transform.position - _transform.position).sqrMagnitude)
            {
                nearestEnemy = enemy;
            }
        }

        _playerInventory.GetEquippedGun().Shoot(nearestEnemy.transform);
    }
 
    public void Death()
    {
        GameController.instance.PlayerDeath();
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage; // !!!
        _healthBar.SetHealth(_currentHealth);

        if (_currentHealth <= 0)
        {
            Death(); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>())
        {
            _enemiesAround.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            _enemiesAround.Remove(collision.GetComponent<Enemy>());
        }
    }
}

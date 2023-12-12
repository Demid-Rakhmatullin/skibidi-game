using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicToiletEnemyController : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int health;
    [SerializeField] float attackCooldown;
    [SerializeField] float attackInitialDelay;

    [SerializeField] EnemyChaseTarget chaseTarget;
    [SerializeField] Animator animator;
    [SerializeField] HealthBar healthBar;

    private EnemyAttackAnimBehaviour _attackBehaviour;
    private float _lastAttackTime;
    private int _currentHealth;

    public EnemyChaseTarget ChaseTarget => chaseTarget;

    void Awake()
    {
        _attackBehaviour = animator.GetBehaviour<EnemyAttackAnimBehaviour>();
        chaseTarget.Init(this);

        _currentHealth = health;
        healthBar.SetMaxValue(health);
        UpdateHealthBar();
    }

    void OnEnable()
    {
        _attackBehaviour.OnAttackEnd += AttackBehaviour_OnAttackEnd;
    }

    void OnDisable()
    {
        _attackBehaviour.OnAttackEnd -= AttackBehaviour_OnAttackEnd;
    }

    void Update()
    {
        Attack();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        UpdateHealthBar();

        if (_currentHealth <= 0)
        {
            animator.SetTrigger("death");
            LevelController.Instance.EnemyDead();

            Destroy(gameObject, 2f);
        }
    }

    private void AttackBehaviour_OnAttackEnd(object sender, EventArgs e)
    {
        if (chaseTarget.Player != null)
            chaseTarget.Player.TakeDamage(damage);
    }

    private void Attack()
    {
        if (chaseTarget.PlayerInside && chaseTarget.Player.IsAlive)
        {
            if (Time.time <= chaseTarget.PlayerEnterTime + attackInitialDelay)
                return;

            if (Time.time - _lastAttackTime > attackCooldown)
            {
                animator.SetTrigger("attack");
                _lastAttackTime = Time.time;
            }
        }
    }

    private void UpdateHealthBar()
        => healthBar.SetValue(_currentHealth);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int maxHealth;
    [SerializeField] int punchDamage;

    [SerializeField] Animator animator;
    [SerializeField] PlayerAnimationEvents animationEvents;
    [SerializeField] HealthBar healthBar;

    private EnemyChaseTarget _chaseTarget;
    private PlayerBlockAnimBehaviour _blockBehaviour;
    private PlayerAttackAnimBehaviour _attackBehaviour;

    public bool IsAlive => _currentHealth > 0;

    private bool _running;
    private bool _attacking;
    private bool _blocking;
    private int _currentHealth;

    private void Awake()
    {
        _blockBehaviour = animator.GetBehaviour<PlayerBlockAnimBehaviour>();
        _attackBehaviour = animator.GetBehaviour<PlayerAttackAnimBehaviour>();

        SetFullHp();
    }

    void Update()
    {
        Chase();
    }

    void OnEnable()
    {
        _blockBehaviour.OnBlockEnd += BlockBehaviour_OnBlockEnd;
        _attackBehaviour.OnAttackEnd += AttackBehaviour_OnAttackEnd;
        animationEvents.OnAttackHit += AnimationEvents_OnAttackHit;
    }

    void OnDisable()
    {
        _blockBehaviour.OnBlockEnd -= BlockBehaviour_OnBlockEnd;
        _attackBehaviour.OnAttackEnd -= AttackBehaviour_OnAttackEnd;
    }

    public void Chase()
    {
        if (_chaseTarget == null)
            return;

        if (Vector3.Distance(transform.position, _chaseTarget.transform.position) > 0.2f)
        {
            var dir = (_chaseTarget.transform.position - transform.position).normalized;
            transform.Translate(speed * Time.deltaTime * dir);
            animator.SetBool("run", true);

            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation((_chaseTarget.Enemy.transform.position - transform.position)),
                20f * Time.deltaTime);
        }
        else if (_running)
        {
            _running = false;
            animator.SetBool("run", false);
        }
    }

    public void SetChaseTarget(EnemyChaseTarget target)
    {
        _running = true;
        _chaseTarget = null;

        StartCoroutine(SetChaseCoroutine(target));
    }

    public void Attack()
    {
        if (_attacking || _running)
            return;

        animator.SetTrigger("punch");
        _attacking = true;
    }

    public void Block()
    {
        if (_blocking || _attacking || _running)
            return;

        animator.SetTrigger("block");
        _blocking = true;
    }

    public void TakeDamage(int damage)
    {
        if (_blocking)
            return;

        _currentHealth -= damage;
        UpdateHealthBar();

        if (_currentHealth <= 0)
        {
            animator.SetBool("death", true);
            LevelController.Instance.PlayerDead();
        }    
    }

    public void Ressurect()
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        _chaseTarget = null;
        SetFullHp();

        animator.SetBool("death", false);
    }

    private IEnumerator SetChaseCoroutine(EnemyChaseTarget target)
    {
        yield return new WaitForSeconds(0.5f);

        _chaseTarget = target;
    }

    private void BlockBehaviour_OnBlockEnd(object sender, EventArgs e)
        => _blocking = false;

    private void AttackBehaviour_OnAttackEnd(object sender, EventArgs e)
        => _attacking = false;

    private void AnimationEvents_OnAttackHit()
    {
        if (_chaseTarget != null && _chaseTarget.Enemy != null)
            _chaseTarget.Enemy.TakeDamage(punchDamage);
    }

    private void SetFullHp()
    {
        healthBar.SetMaxValue(maxHealth);
        _currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
        => healthBar.SetValue(_currentHealth);
}

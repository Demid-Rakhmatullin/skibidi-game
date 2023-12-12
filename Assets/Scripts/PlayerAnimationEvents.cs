using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public event Action OnAttackHit; 

    private void AttackHit()
    {
        OnAttackHit?.Invoke();
    }
}

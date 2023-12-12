using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimBehaviour : StateMachineBehaviour
{
    public event EventHandler OnAttackEnd;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnAttackEnd?.Invoke(this, EventArgs.Empty);
    }
}

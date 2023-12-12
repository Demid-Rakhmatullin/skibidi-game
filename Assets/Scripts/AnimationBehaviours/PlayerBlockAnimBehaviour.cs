using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockAnimBehaviour : StateMachineBehaviour
{
    public event EventHandler OnBlockEnd;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnBlockEnd?.Invoke(this, EventArgs.Empty);
    }
}

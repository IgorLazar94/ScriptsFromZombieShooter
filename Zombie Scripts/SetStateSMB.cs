using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStateSMB : StateMachineBehaviour
{

    public int numberAnimationrandom;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        int randState = Random.Range(1, numberAnimationrandom + 1);
        animator.SetInteger(TagManager.RANDOM_PARAMETER, randState);

    }
}

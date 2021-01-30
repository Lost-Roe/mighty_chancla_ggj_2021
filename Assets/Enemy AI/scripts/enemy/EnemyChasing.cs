using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : StateMachineBehaviour
{
    private EnemyAI ai;
    private Animator anim;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.gameObject.GetComponent<EnemyAI>();
        ai.deathTimer = ai.deathTime;
        ai.deathScreen.color = new Color(ai.deathScreen.color.r, ai.deathScreen.color.g, ai.deathScreen.color.b, 0);
        anim = animator;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float curDistance = ai.DistanceFromTarget();
        if (curDistance > ai.chaseDistance)
        {
            anim.SetBool("searching", true);
            anim.SetBool("chasing", false);
        }
        else
        {
            anim.SetBool("jumpScare", ai.AttemptJumpscare());
            if (curDistance <= ai.reachDistance) {
                anim.SetBool("reached", true);
            } else
            {
                ai.ChaseTowards();
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

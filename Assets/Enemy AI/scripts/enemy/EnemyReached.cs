using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReached : StateMachineBehaviour
{
    private EnemyAI ai;
    private Animator anim;
    private Color screenColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.gameObject.GetComponent<EnemyAI>();
        anim = animator;
        ai.deathTimer = ai.deathTime;
        screenColor = new Color(ai.deathScreen.color.r, ai.deathScreen.color.g, ai.deathScreen.color.b, 0);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float curDistance = ai.DistanceFromTarget();
        if (curDistance > ai.reachDistance)
        {
            anim.SetBool("chasing", true);
            anim.SetBool("reached", false);
        }
        else
        {
            ai.deathTimer = ai.deathTimer - (1 * Time.deltaTime);
            float percentage = ai.deathTimer / ai.deathTime;
            screenColor = new Color(ai.deathScreen.color.r, ai.deathScreen.color.g, ai.deathScreen.color.b, 1 - percentage);
            ai.Stop();
            ai.deathScreen.color = screenColor;
            if(ai.deathTimer <= 0)
            {
                anim.SetBool("gameOver", true);
                Debug.Log("Game Over");
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

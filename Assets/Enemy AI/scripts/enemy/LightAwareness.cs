using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAwareness : EnemySense
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            EnemyAnimator.SetBool(targetBool,true);  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnemyAnimator.SetBool(targetBool, false);
        }
    }
}

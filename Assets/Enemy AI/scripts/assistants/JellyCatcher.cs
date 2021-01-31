using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyCatcher : MonoBehaviour
{
    private JellyFish jelly;
    private GameDirector director;
    private bool reached;
    // Start is called before the first frame update
    void Start()
    {
        reached = false;
        director = GameObject.Find("_GameDirector_").GetComponent<GameDirector>();
        jelly = gameObject.GetComponentInParent<JellyFish>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(reached == false)
        {
            if(other.gameObject != transform.parent && other.gameObject.tag == "Player")
            {
                jelly.SetTarget(director.GetJellyTarget());
                director.currentJellies++;
                jelly.Reached();
                jelly.gameObject.tag = "Untagged";
                director.CheckForInterestPoints();
                reached = true;
            }
        }
    }
}

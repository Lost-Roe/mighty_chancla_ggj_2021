using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMixer : MonoBehaviour
{
    private GameDirector director;
    private string mood;
    public AmbientMix good;
    public AmbientMix evil;
    // Start is called before the first frame update
    void Start()
    {
        director = FindObjectOfType<GameDirector>();
        good.Initialize(true);
        evil.Initialize(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(director.anxiety > 0.8f && director.intuition > 0.8f)
        {
            if(mood != "good")
            {
                evil.Play();
                good.Mute();
                mood = "good";
            }
        }
        else
        {
            if (mood != "evil")
            {
                evil.Mute();
                good.Play();
                mood = "evil";
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseIndicator : MonoBehaviour
{
    private GameDirector director;
    private float curPulseSpeed;
    private float pulseSpeedRange;
    private Color curColor;

    public Color goodColor;
    public Color badColor;


    public ParticleSystem pulse;
    public float minPulseSpeed;
    public float maxPulseSpeed;

    public AudioLayer neutalBeat;
    public AudioLayer enemyBeat;

    private float curPitch;
    private float pitchRange;
    public float minPitch;
    public float maxPitch;
    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.FindObjectOfType<GameDirector>();
        pulseSpeedRange = maxPulseSpeed - minPulseSpeed;
        pitchRange = maxPitch - minPitch;

        neutalBeat.Initialize();
        enemyBeat.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        var main = pulse.main;
        //Color
        float intuitionAprox;

        if(director.intuition >= 0.9f)
            intuitionAprox = 1;  
        else if(director.intuition <= 0.1f)
            intuitionAprox = 0;
        else
            intuitionAprox = director.intuition;

        curColor = Color.Lerp(goodColor, badColor, intuitionAprox);
        main.startColor = curColor;

        //Speed
        curPulseSpeed = minPulseSpeed + (pulseSpeedRange * director.anxiety);
        main.simulationSpeed = curPulseSpeed;

        //Sound
        curPitch = minPitch + (pitchRange * director.anxiety);
        enemyBeat.SetPitch(curPitch);

        neutalBeat.SetPitch(curPitch);
        if (director.anxiety > 0.5f)
        {
            enemyBeat.SetVolume(director.intuition);
            neutalBeat.SetVolume(director.intuition);
        } else
        {
            enemyBeat.SetVolume(0.2f);
            neutalBeat.SetVolume(0.2f);
        }
        
    }
}

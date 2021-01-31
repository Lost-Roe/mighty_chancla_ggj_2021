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

    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.FindObjectOfType<GameDirector>();
        pulseSpeedRange = maxPulseSpeed - minPulseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        var main = pulse.main;

        float intuitionAprox;

        if(director.intuition >= 0.9f)
            intuitionAprox = 1;  
        else if(director.intuition <= 0.1f)
            intuitionAprox = 0;
        else
            intuitionAprox = director.intuition;


        curColor = Color.Lerp(goodColor, badColor, intuitionAprox);
        main.startColor = curColor;

        curPulseSpeed = minPulseSpeed + (pulseSpeedRange * director.anxiety);
        main.simulationSpeed = curPulseSpeed;
    }
}

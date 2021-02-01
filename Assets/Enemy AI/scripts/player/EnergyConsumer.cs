using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConsumer : MonoBehaviour
{
    [SerializeField]
    private EnergySystem energySystem;
    [SerializeField]
    private Light right_eye;
    [SerializeField]
    private Light left_eye;

    private float maxLightIntensity;

    public float consumptionRate;
    

    // Start is called before the first frame update
    void Start()
    {
        maxLightIntensity = right_eye.intensity;
        energySystem.ResetEnergySystem();
        InvokeRepeating("ConsumeEnergy", consumptionRate, consumptionRate);
    }

    public void ConsumeEnergy()
    {
        var e = energySystem.currentEnergy;

        if(energySystem.stateManager.gameState != GameState.Paused)
        {
            energySystem.currentEnergy = Mathf.Clamp(e + (1 * energySystem.consuptionMultiplier),0,energySystem.maxEnergy);
        }
    }
    // Update is called once per frame
    void Update()
    {
        right_eye.intensity = Mathf.Clamp(maxLightIntensity * (energySystem.currentEnergy / 100), 0, maxLightIntensity);
        left_eye.intensity = right_eye.intensity;
    }
}

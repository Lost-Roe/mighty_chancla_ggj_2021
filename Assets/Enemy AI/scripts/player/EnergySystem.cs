using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnergyState { Consuming, Charging, Dashing, ChargingDashing}

[CreateAssetMenu(fileName = "New Energy System", menuName = "GGJ2021/Systems/Energy System")]
public class EnergySystem : ScriptableObject
{
    const float maxEn = 100;
    public PauseSystem stateManager;
    public float consuptionMultiplier;
    public EnergyState state;
    public float currentEnergy;

    public float maxEnergy { get { return maxEn; } }

    public void ResetEnergySystem()
    {
        currentEnergy = maxEnergy;
        SetState(EnergyState.Consuming);
    }

    public void SetState(EnergyState s)
    {
        switch (s)
        {
            case EnergyState.Charging:
                consuptionMultiplier = 5;
                break;
            case EnergyState.Consuming:
                consuptionMultiplier = -0.2f;
                break;
            case EnergyState.Dashing:
                consuptionMultiplier = -5;
                break;
            case EnergyState.ChargingDashing:
                consuptionMultiplier = 2;
                break;
            default:
                consuptionMultiplier = -0.2f;
                break;
        }
        state = s;
    }


}

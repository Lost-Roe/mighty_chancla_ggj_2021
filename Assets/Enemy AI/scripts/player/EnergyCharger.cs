using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharger : MonoBehaviour
{
    [SerializeField]
    private EnergySystem energySystem;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Charger")
        {
            if(energySystem.state == EnergyState.Dashing || energySystem.state == EnergyState.ChargingDashing)
            {
                energySystem.SetState(EnergyState.ChargingDashing);
            } else
            {
                energySystem.SetState(EnergyState.Charging);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Charger")
        {
            if(energySystem.state != EnergyState.ChargingDashing)
            {
                energySystem.SetState(EnergyState.Consuming);
            } else
            {
                energySystem.SetState(EnergyState.Dashing);
            }

        }
    }
}

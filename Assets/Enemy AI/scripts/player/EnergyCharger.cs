using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCharger : MonoBehaviour
{
    [SerializeField]
    private EnergySystem energySystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Charger")
        {
            energySystem.SetState(EnergyState.Charging);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Charger")
        {
            energySystem.SetState(EnergyState.Consuming);
        }
    }
}

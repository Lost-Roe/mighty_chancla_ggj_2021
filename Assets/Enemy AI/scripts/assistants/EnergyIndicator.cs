using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyIndicator : MonoBehaviour
{
    [SerializeField]
    private EnergySystem energySystem;
    public GameObject chargingIcon;
    public Image percentageAmount;


    // Update is called once per frame
    void Update()
    {
        percentageAmount.fillAmount = energySystem.currentEnergy / 100;
        switch (energySystem.state)
        {
            case EnergyState.Charging:
                chargingIcon.SetActive(true);
                percentageAmount.gameObject.SetActive(false);
                break;
            case EnergyState.Consuming:
                chargingIcon.SetActive(false);
                percentageAmount.gameObject.SetActive(true);
                break;
            case EnergyState.Dashing:
                chargingIcon.SetActive(false);
                percentageAmount.gameObject.SetActive(true);
                break;
            case EnergyState.ChargingDashing:
                chargingIcon.SetActive(true);
                percentageAmount.gameObject.SetActive(false);
                break;
        }
    }
}

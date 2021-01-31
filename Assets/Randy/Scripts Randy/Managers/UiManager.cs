using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UiName { MainMenu, InGame, Pause, Credits}
public class UiManager : MonoBehaviour
{
    [SerializeField] private UiName OnlyForEditorUiName;

    [Header("Ui Panels")]
    [SerializeField] private GameObject[] UiPanels = null;

    public void Activate(UiName UiName)
    {
        UiPanels[0].SetActive(UiName == UiName.MainMenu);
        UiPanels[1].SetActive(UiName == UiName.InGame);
        UiPanels[2].SetActive(UiName == UiName.Pause);
        UiPanels[3].SetActive(UiName == UiName.Credits);
    }
}

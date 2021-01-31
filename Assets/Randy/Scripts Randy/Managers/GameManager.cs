using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private AudioManager _audioManager = null;
    [SerializeField] private UiManager _uiManager = null;
    [SerializeField] private TransitionsManager _transitionsManager = null;

    [Header("Only Debug")]
    [Tooltip("Se utiliza principalmente para debugs BORRAR CUANDO SE PUBLIQUE")] public bool clearValuesOnLoad = false;

    [Header("Persistent Objects")]
    [SerializeField] [Tooltip("Objetos que no se van a borrar en todas las escenas")] private GameObject[] persistetObjects = null;

    [Header("Scenes")]
    [SerializeField] [Tooltip("Nombre de las escenas a utilizar")] private string[] scenes = null;

    [SerializeField] private bool isInGame = false;

    private void Awake()
    {
        foreach (GameObject obj in persistetObjects)
            Object.DontDestroyOnLoad(obj);

        LoadMainMenuStart();
    }

    #region Main Menu
    private void LoadMainMenuStart()
    {
        SceneManager.LoadScene("Scenes/" + scenes[0]);
        _uiManager.Activate(UiName.MainMenu);
        _transitionsManager.TransitionMusic(0);
    }

    public void LoadMainMenu()
    {
        _transitionsManager.TransitionSceneMainMenu();
        _transitionsManager.TransitionMusic(0);
    }

    public void BackMainMenu()
    {
        _uiManager.Activate(UiName.MainMenu);
    }
    #endregion

    #region Level
    public void LoadLevel(int idx)
    {
        _transitionsManager.TransitionScene(idx);
        _transitionsManager.TransitionMusic(1);
    }
    #endregion

    public void OpenCredits()
    {
        _uiManager.Activate(UiName.Credits);
    }

    #region Exit
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion
}

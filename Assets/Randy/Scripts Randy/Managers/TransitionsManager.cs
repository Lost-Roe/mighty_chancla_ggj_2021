using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionsManager : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager = null;
    [SerializeField] private UiManager _uiManager = null;

    [Header("Animations & transitions")]
    [SerializeField] [Tooltip("Para la transicion de escenas")] private Animator transitionScene = null;
    [SerializeField] [Tooltip("Para la transicion de musica de las escenas")] private Animator transitionMusic = null;
    [SerializeField] [Tooltip("Para el tiempo de transicion")] private float transitionTime = 0f;

    public void TransitionSceneMainMenu()
    {
        StartCoroutine(LoadTransitionSceneMainMenu());
    }

    public void TransitionScene(int idx)
    {
        StartCoroutine(LoadTransitionScene(idx));
    }

    public void TransitionMusic(int idx)
    {
        StartCoroutine(LoadTransitionMusic(idx));
    }

    IEnumerator LoadTransitionSceneMainMenu()
    {
        transitionScene.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        _uiManager.Activate(UiName.MainMenu);
        SceneManager.LoadScene(1);
        transitionScene.SetTrigger("End");
    }

    IEnumerator LoadTransitionScene(int idx)
    {
        transitionScene.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        _uiManager.Activate(UiName.InGame);
        SceneManager.LoadScene(idx);
        transitionScene.SetTrigger("End");
    }

    IEnumerator LoadTransitionMusic(int idx)
    {
        transitionMusic.SetTrigger("FadeStart");
        yield return new WaitForSeconds(transitionTime);
        _audioManager._musicSource.clip = _audioManager._music[idx];
        _audioManager._musicSource.Play();
        transitionMusic.SetTrigger("FadeEnd");
    }
}

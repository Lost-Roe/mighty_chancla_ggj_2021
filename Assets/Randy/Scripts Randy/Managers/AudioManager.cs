using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Añadir las canciones en el orden de los niveles")] public AudioClip[] _music;

    public AudioMixer audioMixer = null;

    public AudioSource _musicSource;
    [Header("Sound Efects")]
    [SerializeField] private AudioSource button;

    public void ButtonPlay()
    {
        button.Play();
    }
}

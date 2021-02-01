using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioLayer
{
    [SerializeField]
    private AudioSource audio;
    private float baseVolume;

    public void Initialize()
    {
        baseVolume = audio.volume;
    }

    public float CurrentVolume()
    {
        return audio.volume;
    }

    public void SetVolume(float vol)
    {
        audio.volume = Mathf.Clamp(baseVolume * vol, 0, 1);
    }

    public void SetPitch(float pitch)
    {
        audio.pitch = pitch;
    }
}

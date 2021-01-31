using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMix : MonoBehaviour
{
    public List<AudioLayer> mixBase;
    public List<AudioLayer> extraSounds;
    public float randomCyle;
    public float fadeTime;
    public bool[] triggers;
    public bool mute;

    public void Initialize(bool muted)
    {
        int c = 0;

        triggers = new bool[extraSounds.Count];
        foreach (AudioLayer a in mixBase)
        {
            a.Initialize();
            if (muted)
                a.SetVolume(0);
        }

        foreach (AudioLayer e in extraSounds)
        {
            e.Initialize();
            TurnOff(c);
            c++;
        }

        InvokeRepeating("RandomToggle", randomCyle, randomCyle);

    }

    private void RandomToggle()
    {
        if(mute == false)
        {
            int rnd = Random.Range(0, extraSounds.Count);
            StartCoroutine(ToggleLayer(rnd));
        }
    }

    public void Mute()
    {
        mute = true;
        int i = 0;

        StopAllCoroutines();
        foreach(AudioLayer a in mixBase)
        {
            StartCoroutine(FadeOut(a));
        }

        foreach(AudioLayer e in extraSounds)
        {
            StartCoroutine(FadeOut(e));
            triggers[i] = false;
            i++;
        }
    }

    public void Play()
    {
        mute = false;
        int i = 0;

        StopAllCoroutines();
        foreach (AudioLayer a in mixBase)
        {
            StartCoroutine(FadeIn(a));
        }

        foreach (AudioLayer e in extraSounds)
        {
            StartCoroutine(FadeIn(e));
            triggers[i] = true;
            i++;
        }
    }

    private IEnumerator FadeOut(AudioLayer layer)
    {
        float min = layer.CurrentVolume();
        do
        {
            min = min - 0.1f;
            layer.SetVolume(min);
            yield return new WaitForSeconds(fadeTime / 10);
        } while (min >= 0);
        yield break;
    }

    private IEnumerator FadeIn(AudioLayer layer)
    {
        float min = layer.CurrentVolume();
        do
        {
            min = min + 0.1f;
            layer.SetVolume(min);
            yield return new WaitForSeconds(fadeTime / 10);
        } while (min <= 1);
        yield break;
    }

    private IEnumerator ToggleLayer(int index)
    {
        if(triggers[index] == false)
        {
            triggers[index] = true;
            float mul = 0;
            do
            {
                mul += 0.1f;
                extraSounds[index].SetVolume(mul);
                yield return new WaitForSeconds(fadeTime / 10);
            } while (mul <= 1);
            yield break;
        }
        if(triggers[index] == true)
        {
            triggers[index] = false;
            float min = 1;
            do
            {
                min = min - 0.1f;
                extraSounds[index].SetVolume(min);
                yield return new WaitForSeconds(fadeTime / 10);
            } while (min >= 0);
            yield break;
        }
    }

    public void TurnOff(int index)
    {
        extraSounds[index].SetVolume(0);
        triggers[index] = false;
    }
}

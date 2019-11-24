using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    public float frequency;

    public float frequency2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySound();
    }

    public void PlayOutAudio()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    public void PlaySound()
    {
        outAudioClip = CreateToneAudioClip(frequency);
        PlayOutAudio();
    }

    private AudioClip CreateToneAudioClip(float frequency)
    {
        float sampleDurationSecs = 1.0f;
        int sampleRate = 44100;
        int sampleLength = (int)((float)sampleRate * sampleDurationSecs);
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            //https://twistedwave.com/online

            float s1 = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));

            float s2 = Mathf.Sin(2.0f * Mathf.PI * frequency2 * ((float)i / (float)sampleRate));

            float finalValue = s1 + s2;
           



            float v = finalValue * maxValue;
            samples[i] = v;


        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }

}

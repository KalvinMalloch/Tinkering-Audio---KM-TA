
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineSlider : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    public float frequency;

    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOutAudio()
    {
        audioSource.PlayOneShot(outAudioClip);

    }

    public void FrequencyAdjusted(float frequencytemp)
    {
        // Frequency on sine slider is adjusted
        frequency = frequencytemp;
        outAudioClip = CreateToneAudioClip(frequency, 0.1f);
        PlayOutAudio();
    }

    public void ButtonPressed()
    {
        // Sine button is pressed
        outAudioClip = CreateToneAudioClip(frequency,1.5f);
        PlayOutAudio();
    }
        

    private AudioClip CreateToneAudioClip(float frequency , float duration)
    {
        float sampleDurationSecs = duration;
        int sampleRate = 44100;
        int sampleLength = (int)((float)sampleRate * sampleDurationSecs);
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            // Generates the Sine Wave
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));  

        

            float v = s * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }
}


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
        frequency = frequencytemp;
        outAudioClip = CreateToneAudioClip(frequency, 0.1f);
        PlayOutAudio();
    }

    public void ButtonPressed()
    {
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
            // sine wave
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));  

            // square wave
            //float s = Mathf.Sign(Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate)));


            // sawtooth
            //float s = Mathf.Abs(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));

        

            float v = s * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }
    //return Mathf.Sign ( Mathf.Sin(currentphase * 2 * Math.PI));

}

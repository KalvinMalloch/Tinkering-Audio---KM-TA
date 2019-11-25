using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTobyTest : MonoBehaviour
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
        outAudioClip = CreateToneAudioClip(frequency);
        PlayOutAudio();
    }

    public void ButtonPressed()
    {
        outAudioClip = CreateToneAudioClip(frequency);
        PlayOutAudio();
    }
        

    private AudioClip CreateToneAudioClip(float frequency)
    {
        int sampleDurationSecs = 1;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            // sine wave
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));  

            // square wave
            //float s = Mathf.Sign(Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate)));


            // testing sawtooth
            //float s = Mathf.Abs(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));

        

            float v = s * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }
    //return Mathf.Sign ( Mathf.Sin(currentphase * 2 * Math.PI));

}

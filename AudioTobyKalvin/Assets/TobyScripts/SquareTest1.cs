using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTest1 : MonoBehaviour
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
        SaveWavUtil.Save("D:\\New folder\\Tinkering-Audio---KM-TA\\AudioTobyKalvin\\Assets\\test.wav", outAudioClip);
    }


    private AudioClip CreateToneAudioClip(float frequency)
    {
        float sampleDurationSecs = 0.2f;
        int sampleRate = 44100;
        int sampleLength = (int)((float)sampleRate * sampleDurationSecs);
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            // sine wave
            //float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));  

            //float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));




            //https://twistedwave.com/online


            float s1 = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float s2 = Mathf.Sin(2.0f * (Mathf.PI / 3) * frequency * (((float)i * 3) / (float)sampleRate));
            // float s3 = Mathf.Sin(2.0f * (Mathf.PI * 5) * frequency * (((float)i * 5) / (float)sampleRate));
            //float s4 = Mathf.Sin(2.0f * (Mathf.PI * 7) * frequency * (((float)i * 7) / (float)sampleRate));



            float finalValue = s1 + s2;
                //+ s3 + s4;

            // square wave
            //float s = Mathf.Sign(Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate)));


            // testing sawtooth
            //float s = Mathf.Abs(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));



            float v = finalValue * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }
    //return Mathf.Sign ( Mathf.Sin(currentphase * 2 * Math.PI));



  

}

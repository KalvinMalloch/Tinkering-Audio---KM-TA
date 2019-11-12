using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTobyTest : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    public int frequency1;

    public int frequency2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(frequency1);
        PlayOutAudio();
        outAudioClip = CreateToneAudioClip(frequency2);
        PlayOutAudio();
        //outAudioClip = CreateToneAudioClip(48);
        //PlayOutAudio();
    }

    public void PlayOutAudio()
    {
        audioSource.PlayOneShot(outAudioClip);

    }

    private AudioClip CreateToneAudioClip(int frequency)
    {
        int sampleDurationSecs = 1;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            //float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));  
            float s = Mathf.Sign(Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate)));
            //float s = Mathf.Abs(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));

        

            float v = s * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }
    //return Mathf.Sign ( Mathf.Sin(currentphase * 2 * Math.PI));

}

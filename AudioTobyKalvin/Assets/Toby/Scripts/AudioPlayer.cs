// Copyright MIT License 2019 K&T Team 27
// Author: Toby Atkinson
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    

    // auido
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public float frequency;

    public float posotiveFrequency;
    public float negativeFrequency;
    private bool frequencyRecentlyChanged;

    //volume
    private float volumePercentage = 0.5f;
    private bool volumeRecentlyChanged;


    //waves 
    enum WaveType { Sine, Sqaure };
    WaveType currentSelectedwave;
    public bool waveRecentlyChanged;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = volumePercentage;
       

    }

    public void PlayAudio()
    {
        outAudioClip = CreateAudioWave(frequency, 0.5f);
        audioSource.PlayOneShot(outAudioClip);
    }

    public void PlayOutAudio()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    public void ChangeVolume(float newVolumeAnount)
    {
        volumePercentage = newVolumeAnount;
        audioSource.volume = volumePercentage;

        if(!volumeRecentlyChanged)
        {
            StartCoroutine(PosotiveSound());
            StartCoroutine(VolumeChangedDelay());
        }
           
        
    }

    private IEnumerator VolumeChangedDelay()
    {
        volumeRecentlyChanged = true;
        yield return new WaitForSeconds(0.25f);
        volumeRecentlyChanged = false;
    }

    public void WaveTypeChanged(float sliderPosition)
    {
        if(sliderPosition <= 0.5)
        {
            // Slider on left Sine
            if(currentSelectedwave == WaveType.Sqaure)
            {
                currentSelectedwave = WaveType.Sine;
                StartCoroutine(PosotiveSound());
                if (!waveRecentlyChanged)
                {
                    StartCoroutine(PosotiveSound());
                    StartCoroutine(WaveChangedDelay());
                }
            }
        }
        else
        {
            // Slider on right square
            if(currentSelectedwave == WaveType.Sine)
            {
                currentSelectedwave = WaveType.Sqaure;
                if (!waveRecentlyChanged)
                {
                    StartCoroutine(PosotiveSound());
                    StartCoroutine(WaveChangedDelay());
                }
            }
        }
    }

    private IEnumerator WaveChangedDelay()
    {
        waveRecentlyChanged = true;
        yield return new WaitForSeconds(0.15f);
        waveRecentlyChanged = false;
    }


    public IEnumerator PosotiveSound()
    {
        outAudioClip = CreateAudioWave(posotiveFrequency, 0.13f);
        audioSource.PlayOneShot(outAudioClip);
        yield return new WaitForSeconds(0.13f);
        if(currentSelectedwave == WaveType.Sine)
        {
            outAudioClip = CreateAudioWave((posotiveFrequency * 1.4f), 0.1f);
        }
        else
        {
            outAudioClip = CreateAudioWave((posotiveFrequency * 1.2f), 0.1f);
        }
        audioSource.PlayOneShot(outAudioClip);
    }

    public IEnumerator NegativeSound()
    {
        outAudioClip = CreateAudioWave(negativeFrequency, 0.25f);
        audioSource.PlayOneShot(outAudioClip);
        yield return new WaitForSeconds(0.35f);
        if (currentSelectedwave == WaveType.Sine)
        {
            outAudioClip = CreateAudioWave((negativeFrequency * 0.8f), 0.45f);
        }
        else
        {
            outAudioClip = CreateAudioWave((negativeFrequency * 0.8f), 0.45f);
        }
        audioSource.PlayOneShot(outAudioClip);
    }

    public void ChangePosotiveFrequency(float newFrequency)
    {
        posotiveFrequency = newFrequency;
        if(!frequencyRecentlyChanged)
        {
            StartCoroutine(PosotiveSound());
            StartCoroutine(FrequencyChangedDelay());
        }
    }

    public void ChangeNegativeFrequency(float newFrequency)
    {
        negativeFrequency = newFrequency;
        if (!frequencyRecentlyChanged)
        {
            StartCoroutine(NegativeSound());
            StartCoroutine(FrequencyChangedDelay());
        }
    }

    private IEnumerator FrequencyChangedDelay()
    {
        frequencyRecentlyChanged = true;
        yield return new WaitForSeconds(0.25f);
        frequencyRecentlyChanged = false;
    }


  


    private AudioClip CreateAudioWave(float frequency, float duration)
    {
        float sampleDurationSecs = duration;
        int sampleRate = 44100;
        int sampleLength = (int)((float)sampleRate * sampleDurationSecs);
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s;
            // Generates the Sine Wave
            if(currentSelectedwave == WaveType.Sine)
            {
                s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            }
            else
            {
                s = Mathf.Sign(Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate)));
            }

           





            float v = s * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }
}
// Copyright MIT License 2019 K&T Team 27
// Author: Toby Atkinson
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main AudioPlayer class.
/// Allows user to addjust sounds and has
/// them be played out.
/// </summary>
public class AudioPlayer : MonoBehaviour
{
    // Holds the AudioSource component in the canvas
    private AudioSource audioSource;

    // Holds the AudioClip that is used whenever a sound is played
    private AudioClip outAudioClip;

    // Frequency variables that hold the Hz amount in posotive and negative sounds
    private float posotiveFrequency = 100;
    private float negativeFrequency = 35;

    // Addjustable float for how loud the sounds will be
    private float volumePercentage = 0.5f;
   
    // Enums created for the different wave types. 
    enum WaveType { Sine, Sqaure };
    // Wave type currently being used stores as a variable
    WaveType currentSelectedwave;
    
    // Amount of Summative waves stored as int
    private int summativeWaveLevel = 0;

    // Bool used to make sure sounds don't overlap when addjusting frequency, volume or wave type
    private bool soundRecentlyChanged;


    /// <summary>
    /// Gets the AuduiSource and sets the volume on startup.
    /// </summary>
    void Start()
    {
        // Assinging the audioSource variable
        audioSource = GetComponent<AudioSource>();

        // Settings the volume level to middle so it can later be adjusted
        audioSource.volume = volumePercentage;
    }

    /// <summary>
    /// Allows user to change volume with slider.
    /// Includes parameter for new volume that user selects.
    /// </summary>
    /// <param name="newVolumeAnount"></param>
    public void ChangeVolume(float newVolumeAnount)
    {
        // Sets volume variable to new selected amount
        volumePercentage = newVolumeAnount;

        // Sets AudioSource to new volume
        audioSource.volume = volumePercentage;

        // If statement checks if sound has been recently changed
        // This lets sound play when volume changed without overlaps
        if(!soundRecentlyChanged)
        {
            // Plays posotive sound with new volume
            StartCoroutine(PosotiveSound());
            // Starts delay so sounds do not overlap
            StartCoroutine(SoundChangedDelay());
        }
    }

    /// <summary>
    /// Delay to stop sounds from volume slider overlapping.
    /// </summary>
    private IEnumerator SoundChangedDelay()
    {
        // Sets bool to true so no more sounds play
        soundRecentlyChanged = true;
        // Delay while sound plays
        yield return new WaitForSeconds(0.25f);
        // Sets bool to false so more sounds can now play
        soundRecentlyChanged = false;
    }

    /// <summary>
    /// Allows user to change what wave type sounds used with scrollbar.
    /// Includes parameter for new wave type that user selects.
    /// </summary>
    /// <param name="sliderPosition"></param>
    public void WaveTypeChanged(float sliderPosition)
    {
        // If statement to check which side slider is on.
        if(sliderPosition <= 0.5)
        {
            // Slider is on left side so Sine Wave is selected

            // If statement to check if scrollbar has been changed from Sqaure Wave
            // to Sine Wave, or if it has only been touched
            if(currentSelectedwave == WaveType.Sqaure)
            {
                // Changes old Square Wave to newly selected Sine Wave
                currentSelectedwave = WaveType.Sine;
                
                // If statement checks if sound has already been played
                if (!soundRecentlyChanged)
                {
                    // Plays posotive sound with new wave
                    StartCoroutine(PosotiveSound());
                    // Starts delay so sounds don't overlap
                    StartCoroutine(SoundChangedDelay());
                }
            }
        }
        else
        {
            // Slider is on right side so Square Wave is selected

            // If statement checks if slider was previously on Sine Wave
            if(currentSelectedwave == WaveType.Sine)
            {
                // Changes old Sine Wave to newly selected Sine Wave
                currentSelectedwave = WaveType.Sqaure;

                // If statement checks if sound has already been played
                if (!soundRecentlyChanged)
                {
                    // Plays posotive sound with new wave
                    StartCoroutine(PosotiveSound());
                    // Starts delay so sounds don't overlap
                    StartCoroutine(SoundChangedDelay());
                }
            }
        }
    }



    /// <summary>
    /// Plays sound with picked frequency,
    /// then plays shorter sound with slightly higher frequency to show positivity.
    /// </summary>
    public IEnumerator PosotiveSound()
    {
        // Uses CreateAudio Wave function to generate sound wave
        outAudioClip = CreateAudioWave(posotiveFrequency, 0.13f);
        // Plays that sound wave through the AudioSource
        audioSource.PlayOneShot(outAudioClip);
        // Delay while first frequency plays
        yield return new WaitForSeconds(0.13f);

        // After delay if statement to check which Wave Type is selected
        if(currentSelectedwave == WaveType.Sine)
        {
            // If sine wave play generate new sound wave with higher frequency
            outAudioClip = CreateAudioWave((posotiveFrequency * 1.4f), 0.1f);
        }
        else
        {
            // Else if Sqaure wave play generate new sound wave with only slighty higher frequency
            outAudioClip = CreateAudioWave((posotiveFrequency * 1.2f), 0.1f);
            // Square wave is adjusted less then Sine wave due to wave being louder and more noticeable
        }
        // Plays newly generated sound wave
        audioSource.PlayOneShot(outAudioClip);
    }

    /// <summary>
    /// Plays sound with picked frequency,
    /// then plays longer sound with slightly lower frequency to show negativity.
    /// </summary>
    public IEnumerator NegativeSound()
    {
        // Uses CreateAudio Wave function to generate sound wave
        outAudioClip = CreateAudioWave(posotiveFrequency, 0.13f);
        // Plays that sound wave through the AudioSource
        audioSource.PlayOneShot(outAudioClip);
        // Delay while first frequency plays
        yield return new WaitForSeconds(0.35f);

        // After delay if statement to check which Wave Type is selected
        if (currentSelectedwave == WaveType.Sine)
        {
            // If sine wave play generate new sound wave with lower frequency
            outAudioClip = CreateAudioWave((negativeFrequency * 0.8f), 0.45f);
        }
        else
        {
            // Else if Sqaure wave play generate new sound wave with only slighty lower frequency
            outAudioClip = CreateAudioWave((negativeFrequency * 0.7f), 0.45f);
            // Square wave is adjusted less then Sine wave due to wave being louder and more noticeable
        }
        // Plays newly generated sound wave
        audioSource.PlayOneShot(outAudioClip);
    }

    /// <summary>
    /// Allows user to change what frequency is used
    /// to generate and play posotive sounds.
    /// Includes parameter for new frequency that user selects.
    /// </summary>
    /// <param name="newFrequency"></param>
    public void ChangePosotiveFrequency(float newFrequency)
    {
        // Sets the wave frequency to the newly selected frequency
        posotiveFrequency = newFrequency;

        // Checks to see if sound is already being played
        if(!soundRecentlyChanged)
        {
            // If not play posotive sound with newly selected frequency
            StartCoroutine(PosotiveSound());
            // Start delay so sounds don't overlap
            StartCoroutine(SoundChangedDelay());
        }
    }

    /// <summary>
    /// Allows user to change what frequency is used
    /// to generate and play negative sounds.
    /// Includes parameter for new frequency that user selects.
    /// </summary>
    /// <param name="newFrequency"></param>
    public void ChangeNegativeFrequency(float newFrequency)
    {
        // Sets the wave frequency to the newly selected frequency
        negativeFrequency = newFrequency;

        // Checks to see if sound is already being played
        if (!soundRecentlyChanged)
        {
            // If not play negative sound with newly selected frequency
            StartCoroutine(NegativeSound());
            // Start delay so sounds don't overlap
            StartCoroutine(SoundChangedDelay());
        }
    }

    /// <summary>
    /// Allows user to select how many summative 
    /// waves are added when generating Sine waves.
    /// Includes parameter for how many waves selected
    /// </summary>
    /// <param name="newFrequency"></param>
    public void ChangedSummativeWaveAmount(int newSummativeWaveAmount)
    {
        // Sets amount of summative waves to newly selected amount
        summativeWaveLevel = newSummativeWaveAmount;
        // Plays sound with that amount of summative waves
        StartCoroutine(PosotiveSound());
    }

    /// <summary>
    /// Generates sound waves using different amounts of frequency,
    /// duration and different waves.
    /// </summary>
    /// <param name="frequency"></param>
    /// <param name="duration"></param>
    /// <returns>
    /// Returns the audioClip so new sound can be played.
    /// </returns>
    private AudioClip CreateAudioWave(float frequency, float duration)
    {
        // Takes the duration of sound wave to create sample length
        float sampleDurationSecs = duration;
        // Sample rate is set to be used to create sample length
        int sampleRate = 44100;
        // Sample lenght created using both above variables
        int sampleLength = (int)((float)sampleRate * sampleDurationSecs);
        // Max value of wave is set
        float maxValue = 1f / 4f;
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        // For loop used to go through and generate each level of sound wave
        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            // Multiple floats created for if more summative waves are added
            float s = 0;
            float s1 = 0;
            float s2 = 0;
            float s3 = 0;

            // If statement checks which type of wave is selected
            if(currentSelectedwave == WaveType.Sine)
            {
                // If Sine Wave is selected if statements add additional waves if selected by user
                if(summativeWaveLevel == 0)
                {
                    s1 = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
                    s = s1;
                }
                if (summativeWaveLevel >= 1)
                {
                    s2 = Mathf.Sin(2.0f * (Mathf.PI * 6) * (frequency * 0.8f) * ((float)i / (float)sampleRate));
                    s += s2;
                }
                if (summativeWaveLevel >= 2)
                {
                    s3 = Mathf.Sin(2.0f * (Mathf.PI * 9) * (frequency * 0.6f) * ((float)i / (float)sampleRate));
                    s += s3;
                }
            }
            else
            {
                // Else if Sqaure Wave is selected Mathf.Sign is used to set wave to either 0 or 1
                s = Mathf.Sign(Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate)));
            }

            // Multiplies each made sound wave amount so it does not exceed the max value
            float v = s * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        // Returns the generated waves to be played out by the AudioSource
        return audioClip;
    }
}
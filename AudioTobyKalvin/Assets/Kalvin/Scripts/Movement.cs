// MIT License Copyright (c) 2019.
// <author>Kalvin Malloch</author>
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA
// <summary>
// Handles the movement of the player and the sound generation with the interaction
// with other game objects.
// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TAGS
{
    BULLET,
    HEALTH,
    COIN,
}

public class Movement : MonoBehaviour
{
    [Header("Sound Attributes")]
    public float frequency;
    public int sampleLength;

    private AudioSource audioSource;
    private AudioClip outAudioClip;

    [Header("Movement Attributes")]
    public float speed;
    public bool walkSound;
    private Rigidbody2D rigidBody;
    

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
		audioSource = GetComponent<AudioSource>();
		walkSound = true;
    }

    private void Update()
    {
        Move();
    }
	
    /// <summary>
    /// Uses the preset axis to enable movement for the player.
    /// </summary>
	private void Move()
	{
		float moveH = Input.GetAxis("Horizontal");
		float moveV = Input.GetAxis("Vertical");

		Vector2 movement = new Vector2 (moveH, moveV);
		rigidBody.velocity = movement * speed;

		if (moveH != 0 || moveV != 0)
        {
			StartCoroutine(FootSoundDelay());
        }
	}

    /// <summary>
    /// Detects any game objects in the player's collider.
    /// </summary>
    /// <param name="other"></param>
    /// <remarks>
    /// Other refers to whatever the other game object is that is in the collider.
    /// </remarks>
    private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag(TAGS.HEALTH.ToString())) 
		{
			StartCoroutine(HealthSoundDelay());
			Destroy (other.gameObject);
		}
        if (other.CompareTag(TAGS.COIN.ToString()))
        {
            frequency = 1800;
            outAudioClip = CreateAudioClip(frequency);
            audioSource.PlayOneShot(outAudioClip);
            Destroy(other.gameObject);
        }
        if (other.CompareTag(TAGS.BULLET.ToString()))
        {
            StartCoroutine(DamageSoundDelay());
            Destroy(other.gameObject);
        }
    }
	
    /// <summary>
    /// The function that creates the audio clip when called upon, includes parameters.
    /// </summary>
    /// <param name="frequency"></param>
    /// <returns>
    /// Frequency is declared elsewhere so there is variety.
    /// </returns>
	private AudioClip CreateAudioClip(float frequency)
    {
        int sampleRate = 44100;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }

    /// <summary>
    /// Creates and plays a new audio clip with a random frequency after waiting a few seconds.
    /// </summary>
    private IEnumerator FootSoundDelay()
    {
		if (walkSound == true) 
		{
			walkSound = false;
			frequency = Random.Range(100, 200);
			outAudioClip = CreateAudioClip(frequency);
            audioSource.PlayOneShot(outAudioClip);
			yield return new WaitForSeconds(0.3f);
			walkSound = true;
		}
    }
	
    /// <summary>
    /// Creates and plays two consecutive sounds after each other with different frequencys.
    /// </summary>
	private IEnumerator HealthSoundDelay()
    {
		frequency = 700;
		outAudioClip = CreateAudioClip(frequency);
        audioSource.PlayOneShot(outAudioClip);
		yield return new WaitForSeconds(0.15f);
		frequency = 800;
		outAudioClip = CreateAudioClip(frequency);
        audioSource.PlayOneShot(outAudioClip);
    }

    private IEnumerator DamageSoundDelay()
    {
        frequency = 400;
        outAudioClip = CreateAudioClip(frequency);
        audioSource.PlayOneShot(outAudioClip);
        yield return new WaitForSeconds(0.15f);
        frequency = 250;
        outAudioClip = CreateAudioClip(frequency);
        audioSource.PlayOneShot(outAudioClip);
    }
}

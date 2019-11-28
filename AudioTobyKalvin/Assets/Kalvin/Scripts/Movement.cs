// Copyright MIT License 2019 K&T Team 27
// Author: Kalvin Malloch
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA

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
	
    // Basic movement script which starts the movement sound cycle coroutine if any of the movement keys are pressed down.
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
	
	// Basic pickup script which starts the coroutine and deletes the game object when entering their trigger collider.
	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag(TAGS.HEALTH.ToString())) 
		{
			StartCoroutine(HealthSoundDelay());
			Destroy (other.gameObject);
		}
        if (other.CompareTag(TAGS.COIN.ToString()))
        {

            Destroy(other.gameObject);
        }
        if (other.CompareTag(TAGS.BULLET.ToString()))
        {
            StartCoroutine(DamageSoundDelay());
            Destroy(other.gameObject);
        }

    }
	
    // Creates the movement audio clip with a short play length.
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
	
    // Creates and initiates a new audio clip everytime the coroutine is ran. There's an if statement so that multiple audio clips
    // don't overlap each other. Frequency is random between two values so that the footstep sound is different everytime.
    // Short delay between the sound clips to give a foot step impression.
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
	
	// Creates two consecutive sounds with a short delay between both - with different frequencies.
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	private Rigidbody2D rig;
	public float speed;
	public float frequency;
	public bool walkSound;
	private AudioSource audioSource;
    private AudioClip outAudioClip;
	
    void Start()
    {
        rig = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource>();
		walkSound = true;
    }

    void Update()
    {
        Move();
    }
	
	void Move()
	{
		float MoveH = Input.GetAxis ("Horizontal");
		float MoveV = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (MoveH, MoveV);
		rig.velocity = movement * speed;
		if ((Input.GetKey("a")) || (Input.GetKey("w")) || (Input.GetKey("s")) || (Input.GetKey("d")))
        {
			StartCoroutine(FootSoundDelay());
        }
	}
	
	private AudioClip CreateMovementAudioClip(float frequency)
    {
        int sampleRate = 44100;
        int sampleLength = 10000;
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
	
	IEnumerator FootSoundDelay()
    {
		if (walkSound == true) 
		{
			walkSound = false;
			frequency = Random.Range(100, 200);
			outAudioClip = CreateMovementAudioClip(frequency);
            audioSource.PlayOneShot(outAudioClip);
			yield return new WaitForSeconds(0.3f);
			walkSound = true;
		}
    }
}

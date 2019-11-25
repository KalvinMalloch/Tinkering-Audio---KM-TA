using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
	private AudioSource audioSource;
    private AudioClip outAudioClip;
	
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
	
	private AudioClip CreatePickupAudioClip(float frequency)
    {
        int sampleRate = 44100;
        int sampleLength = 10000;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
			frequency = 700;
			
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.gameObject.name == "Health") 
		{
			print ("test");
		}
		
	}
}

// Copyright MIT License 2019 K&T Team 27
// <author> Kalvin Malloch <\author>
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour 
{
    [Header("Bullet Attributes")]
    public float fireRate;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private float offset = 0.0f;
    private float nextFire;

    [Header("Sound Attributes")]
    public float frequency;
    public float secondaryFrequency;
    public int sampleLength;
    public int echoNumber;
 
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

	private void Update() 
    {
        PointToMouse();
        FireGun();
	}

    /// <summary>
    /// Rotates game object to always face the direction of the mouse cursor.
    /// </summary>
    private void PointToMouse()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
    }

    /// <summary>
    /// Instantiates a game object with force while playing and creating an audio clip.
    /// </summary>
    private void FireGun()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            StartCoroutine(ShootSoundEcho());
            nextFire = Time.time + fireRate;

            Rigidbody2D bulletCloneRigidBody = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation).GetComponent<Rigidbody2D>();
            bulletCloneRigidBody.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// The function that creates the audio clip when called upon, includes parameters.
    /// </summary>
    /// <param name="frequency"></param>
    /// <returns>
    /// Frequency is declared in the inspector.
    /// </returns>
    private AudioClip CreateAudioClip(float frequency)
    {
        int sampleRate = 44100;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s1 = Mathf.Sign(Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate)));
            float s2 = Mathf.Sin(2.0f * Mathf.PI * secondaryFrequency * ((float)i / (float)sampleRate));
            float v = s1 + s2 * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }

    /// <summary>
    /// Creates an echo effect for the sound clip.
    /// </summary>
    private IEnumerator ShootSoundEcho()
    {
        frequency = 400;
        secondaryFrequency = 300;
        for (int i = 0; i < echoNumber; i++)
        {
            frequency = frequency / 2;
            secondaryFrequency = secondaryFrequency / 2;
            outAudioClip = CreateAudioClip(frequency);
            audioSource.PlayOneShot(outAudioClip);
            yield return new WaitForSeconds(0.15f);
        }
    }
}

// Copyright MIT License 2019 K&T Team 27
// Author: Kalvin Malloch
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour 
{
	public float fireRate;
    public float frequency;
    public float frequency2;
    private float nextFire;
	private float offset = 0.0f;
    public int sampleLength;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    void Start () 
    {
        audioSource = GetComponent<AudioSource>();
    }

	void Update () 
    {
        PointToMouse();
        FireGun();
	}

    void PointToMouse()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
    }

    void FireGun()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            outAudioClip = CreateAudioClip(frequency);
            audioSource.PlayOneShot(outAudioClip);
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        }
    }

    private AudioClip CreateAudioClip(float frequency)
    {
        int sampleRate = 44100;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s1 = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float s2 = Mathf.Sin(2.0f * Mathf.PI * frequency2 * ((float)i / (float)sampleRate));
            float v = s1 + s2 * maxValue;
            samples[i] = v;
        }
        audioClip.SetData(samples, 0);
        return audioClip;
    }
}

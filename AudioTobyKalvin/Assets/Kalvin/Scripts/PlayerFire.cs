using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float fireRate;
	private float nextFire;
	private float offset = 0.0f;
	public AudioClip playerFire;
	public AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource> ();
	}

	void Update () {
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();
		float rotation_z = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rotation_z + offset);
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			audio.PlayOneShot (playerFire);
			nextFire = Time.time + fireRate;
			Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		}
		
	}
}

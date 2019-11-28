// Copyright MIT License 2019 K&T Team 27
// Author: Kalvin Malloch
// Link To Repository: https://github.com/KalvinMalloch/Tinkering-Audio---KM-TA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMover : MonoBehaviour {

	public float speed;
	private Rigidbody2D rig;

	void Start () 
    {
		rig = GetComponent<Rigidbody2D> ();
		rig.AddForce(transform.right * speed);
	}

	void OnTriggerEnter2D(Collider2D other) 
    {
		if (other.gameObject.name == "Gamespace") 
        {
			Destroy (gameObject);
		}
	}

	void Awake() 
    {
		Destroy (gameObject, 2);
	}
}

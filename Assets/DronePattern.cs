using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePattern : MonoBehaviour {

	public float speed;

	Rigidbody body;
	bool rotate = false;


	void Start(){
		body = GetComponent<Rigidbody> ();
		StartCoroutine ("Rotate");
	}

	IEnumerator Rotate(){
		body.angularVelocity = Vector3.up * speed;
		yield return new WaitForSeconds(0.1f);
	}
}

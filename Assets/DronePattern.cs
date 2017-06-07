using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePattern : MonoBehaviour {

	public float speed;
	public Pattern pattern;

	private Rigidbody body;
	public enum Pattern {RotateAndShoot, SomethingElse};
	private EnemyShootController shootComponent;
	bool rotate = false;

	void Start(){
		body = GetComponent<Rigidbody> ();
		StartCoroutine ("Rotate");
		shootComponent = GetComponentInChildren<EnemyShootController> ();
		PatternSwitch ();
	}

	void Update()
	{
		StartCoroutine ("RotateAndShoot");
	}

	IEnumerator Rotate(){
		body.angularVelocity = Vector3.up * speed;
		yield return new WaitForSeconds(0.1f);
	}



	void PatternSwitch(){
		switch (pattern) {
		case Pattern.RotateAndShoot:
			RotateAndShoot ();
			break;
		case Pattern.SomethingElse:
			break;
		default:
			break;
		}
	}

	IEnumerator RotateAndShoot(){
			Rotate ();
			shootComponent.SpreadShot (5);
		yield return new WaitForSeconds (1.0f);
	}
}

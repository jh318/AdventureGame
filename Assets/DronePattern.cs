using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePattern : MonoBehaviour {

	public float speed;
	public Pattern pattern;

	private Rigidbody body;
	public enum Pattern {RotateAndShoot, IdleAndShoot, SomethingElse};
	private EnemyShootController shootComponent;
	bool rotate = false;

	void Start(){
		body = GetComponent<Rigidbody> ();
		shootComponent = GetComponentInChildren<EnemyShootController> ();
		PatternSwitch ();
	}

	void Update()
	{
	}
		

	void PatternSwitch(){
		switch (pattern) {
			case Pattern.RotateAndShoot:
				StartCoroutine("RotateAndShoot");
				break;
			case Pattern.IdleAndShoot:
				StartCoroutine ("IdleAndShoot");
				break;
			case Pattern.SomethingElse:
				break;
			default:
				break;
		}
	}

	void Rotate(){
		body.angularVelocity = Vector3.up * speed;
	}

	IEnumerator RotateAndShoot(){
		Rotate ();
		while (pattern == Pattern.RotateAndShoot) {
			shootComponent.SpreadShot (3);
			yield return new WaitForSeconds (1.0f);
		}
		PatternSwitch ();
	}

	IEnumerator IdleAndShoot(){
		body.angularVelocity = Vector3.zero;
		while (pattern == Pattern.IdleAndShoot) {
			shootComponent.SpreadShot (3);
			yield return new WaitForSeconds (1.0f);
		}
		PatternSwitch ();
	}
}

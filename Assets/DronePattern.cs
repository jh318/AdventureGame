﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePattern : MonoBehaviour {

	public float speed;
	public Pattern pattern;
	public int projectileCount = 3;

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
		yield return new WaitForSeconds (Random.value);
		while (pattern == Pattern.RotateAndShoot && enabled) {
			shootComponent.SpreadShot (projectileCount);
			yield return new WaitForSeconds (1.0f);
		}
		PatternSwitch ();
	}

	IEnumerator IdleAndShoot(){
		body.angularVelocity = Vector3.zero;
		yield return new WaitForSeconds (Random.value);
		while (pattern == Pattern.IdleAndShoot && enabled) {
			shootComponent.SpreadShot (projectileCount);
			yield return new WaitForSeconds (1.0f);
		}
		PatternSwitch ();
	}
}

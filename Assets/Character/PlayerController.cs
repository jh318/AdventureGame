using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 5;
	public float turnSpeed = 3;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		anim.SetFloat("forwardVelocity", y * speed);
		anim.SetFloat("turnVelocity", x * turnSpeed);
		anim.SetFloat("speed", x*x + y*y);

		if (Input.GetButtonDown ("Jump")) {
			anim.SetTrigger ("playerDeath");
		}
		if (Input.GetButtonDown ("Fire2")) {
			anim.SetTrigger ("fireball");

		}
		if (Input.GetButtonDown ("Fire3")) {
			anim.SetTrigger ("hitReactBackwards");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 5;
	public float maxTurnSpeed = 3.5f;
	public float maxForwardVelocity = 5f;
	public float maxTurnVelocity = 3.5f;

	private Animator anim;
	private HealthController health;
	private Rigidbody body;


	// Use this for initialization
	void Start () {
		health = GetComponent<HealthController> ();
		health.onHealthChanged += AnimateHealth;
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody> ();
	}

	void AnimateHealth(float health, float prevHealth, float maxHealth){
		if (health <= 0) {
			anim.SetTrigger ("playerDeath");
		} else if (health < prevHealth) {
			anim.SetTrigger ("hitReactBackwards");
		}
	}

	// Update is called once per frame
	void Update () {
		Transform cam = Camera.main.transform;
		Vector3 targetRight = Input.GetAxis ("Horizontal") * cam.right;
		Vector3 targetForward = Input.GetAxis ("Vertical") * cam.forward;
		Vector3 target = targetRight + targetForward;
		target.y = 0;

		body.velocity = (target).normalized * maxSpeed;

		Vector3 heading = body.velocity.normalized;
		float forward = Vector3.Dot (heading, transform.forward);

		float speed = body.velocity.magnitude;
		anim.SetFloat ("speed", speed);
		anim.SetFloat ("forwardVelocity", forward * speed);
		anim.SetFloat ("turnVelocity", body.angularVelocity.y);




		//Stupid Anim Stuff
		if (Input.GetButtonDown ("Jump")) {
			anim.SetTrigger ("playerDeath");
		}
		if (Input.GetButtonDown ("Fire2")) {
			anim.SetTrigger ("fireball");

		}
		if (Input.GetButtonDown ("Fire3")) {
			anim.SetTrigger ("pickUpObject");
		}
		if (Input.GetKeyDown (KeyCode.Keypad1))
			anim.SetTrigger ("headSpin");
		
		if (Input.GetKeyDown (KeyCode.Keypad2))
			anim.SetTrigger ("backFlip");
	}
	void FixedUpdate()
	{
		Vector3 v = body.velocity;
		Vector3 heading = v.normalized;
		float speed = v.magnitude;

		float turnSpeed = Vector3.Cross (transform.forward, heading).y * maxTurnSpeed;
		body.angularVelocity = Vector3.up * turnSpeed;
	}


}

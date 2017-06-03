using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSpeed = 5;
	public float maxSpeedChange = 0.3f;
	public float maxTurnSpeed = 3.5f;
	public float projectileSpeed = 5;
	public string projectile = "projectile";
	public GameObject projectileSpawn;
	//public float maxForwardVelocity = 5f;
	//public float maxTurnVelocity = 3.5f;
	public float shootCoolDown = 1.0f;

	private Animator anim;
	private HealthController health;
	private Rigidbody body;
	private Vector3 targetVelocity;
	private float shootCooldownTimer;

	// Use this for initialization
	void Start () {
		health = GetComponent<HealthController> ();
		health.onHealthChanged += AnimateHealth;
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody> ();
		shootCooldownTimer = Time.time - shootCoolDown;
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
		Vector3 targetForward = Input.GetAxis ("Vertical") * cam.up;
		targetVelocity = targetRight + targetForward;
		targetVelocity.y = 0;
		targetVelocity = targetVelocity.normalized * maxSpeed;

		Vector3 heading = body.velocity.normalized;
		float forward = Vector3.Dot (heading, transform.forward);


		float speed = body.velocity.magnitude;
		anim.SetFloat ("speed", speed);
		anim.SetFloat ("forwardVelocity", forward * speed);
		anim.SetFloat ("turnVelocity", body.angularVelocity.y);


		//Stupid Anim Stuff
		//if cooldown time is over, shoot bullet
		if((Time.time - shootCooldownTimer > shootCoolDown) && Input.GetButton("Jump")){
			//anim.SetTrigger ("fireball");
			ShootProjectile ();
			shootCooldownTimer = Time.time;
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
		
		StickShooterControls();
	}
	void FixedUpdate()
	{
		Vector3 v = body.velocity;
		Vector3 heading = v.normalized;
		float speed = v.magnitude;

		Vector3 velocityChange = targetVelocity - v;
		velocityChange = velocityChange.normalized * Mathf.Clamp (velocityChange.magnitude, -maxSpeedChange, maxSpeedChange);
		velocityChange.y = 0;
		body.AddForce (velocityChange, ForceMode.VelocityChange);

		float turnSpeed = Vector3.Cross (transform.forward, heading).y * maxTurnSpeed;
		body.angularVelocity = Vector3.up * turnSpeed;
	}
	void ShootProjectile(){
		GameObject bulletSpawn = Spawner.Spawn (projectile);
		GameObject bullet = Instantiate (bulletSpawn, (projectileSpawn.transform.position), Quaternion.identity);
		Rigidbody bulletBody = bullet.GetComponent<Rigidbody> ();
		bulletBody.velocity = gameObject.transform.forward * projectileSpeed;
	}

	void StickShooterControls(){
		float RightStickX = Input.GetAxis ("RightStickX");
		float RightStickY = Input.GetAxis ("RightStickY");

		if (Mathf.Abs(Input.GetAxis ("RightStickX")) > 0.5f || Mathf.Abs(Input.GetAxis ("RightStickY")) > 0.5f) {
			anim.SetBool ("LockedLocomotion", true);
			Vector3 forward = Camera.main.transform.forward;
			if (Vector3.Dot (forward, Vector3.down) > (.707106f)) {
				forward = Camera.main.transform.up;
			}
			forward.y = 0;
			forward.Normalize ();
		
			Vector3 right = Camera.main.transform.right;
			right.y = 0;
			right.Normalize ();

			Vector3 targetHeading = forward * RightStickY + right * RightStickX;

			transform.forward = targetHeading;
		} else {
			anim.SetBool ("LockedLocomotion", false);
		}
	}
}

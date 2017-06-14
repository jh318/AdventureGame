using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	public float deceleration = 0.15f;

	private Animator anim;
	private HealthController health;
	private Rigidbody body;
	private Vector3 targetVelocity;
	private float shootCooldownTimer;
	private bool playerIsDead = false;
	private bool gameStart = false;
	private CameraTricks cameraTricks;

	// Use this for initialization
	void Start () {
		health = GetComponent<HealthController> ();
		health.onHealthChanged += AnimateHealth;
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody> ();
		shootCooldownTimer = Time.time - shootCoolDown;
		cameraTricks = Camera.main.GetComponent<CameraTricks> ();
		playerIsDead = false;
		gameStart = false;
		PlayIntro ();
	}

	void AnimateHealth(float health, float prevHealth, float maxHealth){
		if (health <= 0) {
			anim.SetTrigger ("playerDeath");
			PlayerDeath ();
			playerIsDead = true;
		} else if (health < prevHealth) {
			anim.SetTrigger ("hitReactBackwards");
		}
	}

	// Update is called once per frame
	void Update () {
		StopIntro ();
		RestartLevel ();
		if (playerIsDead)
			return;
		Transform cam = Camera.main.transform;
		//if camera is pointing down
		Vector3 targetRight;
		Vector3 targetForward;
		if (Vector3.Dot (cam.transform.forward, Vector3.down) > 0.707f) {
			targetRight = Input.GetAxisRaw ("Horizontal") * cam.right;
			targetForward = Input.GetAxisRaw ("Vertical") * cam.up;
		} else { //Camera is side
			targetRight = Input.GetAxisRaw ("Horizontal") * cam.right;
			targetForward = Input.GetAxisRaw ("Vertical") * cam.forward;
		}




		targetVelocity = targetRight + targetForward;
		targetVelocity.y = 0;
		targetVelocity = targetVelocity.normalized * maxSpeed;
		//body.velocity = targetVelocity; // remove me to go back to the old days

		Vector3 heading = body.velocity.normalized;
		float forward = Vector3.Dot (heading, transform.forward);

		if (targetVelocity.magnitude <= 0.1f) {
			targetVelocity = -targetVelocity * deceleration;
		}

		float speed = body.velocity.magnitude;
		anim.SetFloat ("speed", speed);
		anim.SetFloat ("forwardVelocity", forward * speed);
		anim.SetFloat ("turnVelocity", body.angularVelocity.y);


		//Stupid Anim Stuff
		//if cooldown time is over, shoot bullet
		if((Time.time - shootCooldownTimer > shootCoolDown) && (Input.GetButton("Jump") || Input.GetButton("ShootGun"))){
			//anim.SetTrigger ("fireball");
			shootCooldownTimer = Time.time;

			ShootProjectile ();
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
		float speed = v.magnitude; //Comment out here to go back to the old days

		Vector3 velocityChange = targetVelocity - v;
		velocityChange = velocityChange.normalized * Mathf.Clamp (velocityChange.magnitude, -maxSpeedChange, maxSpeedChange);
		velocityChange.y = 0;
		body.AddForce (velocityChange, ForceMode.VelocityChange); //Comment out to here to go back to the old days

		float turnSpeed = Vector3.Cross (transform.forward, heading).y * maxTurnSpeed;
		body.angularVelocity = Vector3.up * turnSpeed;
	}
	void ShootProjectile(){
		GameObject bullet = Spawner.Spawn (projectile);
		bullet.transform.position = projectileSpawn.transform.position;
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

	void PlayerDeath(){
		body.velocity = Vector3.zero;
		KillBox.instance.PlayerHasDied ();

	}

	void RestartLevel(){
		if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("RestartButton")){
			SceneManager.LoadScene ("animationScene1");
			gameStart = false;
		}
	}

	void PlayIntro(){
		TextManager.instance.textBox.text = "Begin Mission";
		TextManager.instance.textBox.gameObject.SetActive (true);
	}

	void StopIntro(){
		if (Input.anyKey)
			gameStart = true;
		if (gameStart) {
			TextManager.instance.textBox.text = "";
			TextManager.instance.textBox.gameObject.SetActive (false);
		}
	}
}

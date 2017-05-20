using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float maxSpeed = 5;
	public float maxTurnSpeedChange = 3.5f;
	public float maxSpeedChange = 0.5f;
	public float wanderAmount = 1;
	public float preAttackDistance = 4;
	public float attackRange = 0.1f;

	private Animator anim;
	private Rigidbody body;
	private Transform target;
	private HealthController playerHealth;

	Vector3 targetVelocity;
	Vector3 targetFacing;


	//Vector3 heading = (target.position - transform.position).normalized;
	//Vector3 targetVelocity = heading * maxSpeed;
	//		Vector3 heading = body.velocity.normalized;
	//			float distanceChange = preAttackDistance - Vector3.Distance (transform.position, target.position);


	void Start () 
	{
		anim = GetComponent<Animator> ();
		body = GetComponent<Rigidbody> ();
		target = GameObject.FindWithTag ("Player").transform;
		StartCoroutine ("WanderState");
		playerHealth = target.gameObject.GetComponent<HealthController> ();

	}

	IEnumerator WanderState()
	{
		while (Vector3.Distance(transform.position, target.position) > 5)
		{
			targetVelocity = transform.forward * maxSpeed + Random.insideUnitSphere * wanderAmount;
			targetFacing = targetVelocity.normalized;
			targetVelocity = transform.right * maxSpeed;
			yield return new WaitForSeconds (0.5f);
		}
		StartCoroutine ("EngagePlayerState");
	}

	IEnumerator EngagePlayerState()
	{
		while (playerHealth.health > 0 && Vector3.Distance (transform.position, target.position) < 10) 
		{
			yield return StartCoroutine ("CirclePlayerState");
			yield return StartCoroutine ("AttackState");

		}
		StartCoroutine ("WanderState");
		yield return new WaitForEndOfFrame ();
	}

	IEnumerator CirclePlayerState() 
	{
		for (float t = 0; t < 3; t += Time.deltaTime) 
		{
			Vector3 diff = target.position - transform.position;
			targetFacing = diff.normalized;
			targetVelocity = -transform.right * maxSpeed + targetFacing * (diff.magnitude - preAttackDistance);
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator AttackState()
	{
		while (Vector3.Distance (transform.position, target.position) > 1)
		{
			Vector3 diff = target.position - transform.position;
			targetFacing = diff.normalized;
			targetVelocity = transform.forward * maxSpeed;
			yield return new WaitForEndOfFrame ();
		}

		targetVelocity = new Vector3 (0, 0, 0);
		anim.SetTrigger ("fireball");
		yield return new WaitForSeconds(2.0f);
	}

	 

	void Update(){
		Vector3 heading = body.velocity.normalized;
		float forward = Vector3.Dot (heading, transform.forward);

		float speed = body.velocity.magnitude;
		anim.SetFloat ("speed", speed);
		anim.SetFloat ("forwardVelocity", forward * speed);
		anim.SetFloat ("turnVelocity", body.angularVelocity.y);
	}
	
	void FixedUpdate () {
		if (target == null) return;



		Vector3 velocityChange = targetVelocity - body.velocity;
		velocityChange.y = 0;
		velocityChange = velocityChange.normalized * Mathf.Clamp (velocityChange.magnitude, 0, maxSpeedChange);
		body.AddForce (velocityChange, ForceMode.VelocityChange);

		float targetTurnSpeed = Vector3.Cross (transform.forward, targetFacing).y * maxTurnSpeedChange;
		float turnSpeedChange = targetTurnSpeed - body.angularVelocity.y;
		turnSpeedChange = Mathf.Clamp (turnSpeedChange, -maxTurnSpeedChange, maxTurnSpeedChange);
		body.AddTorque (Vector3.up * turnSpeedChange, ForceMode.VelocityChange);
	}
}

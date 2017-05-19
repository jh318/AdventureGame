using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float maxSpeed = 5;
	public float maxTurnSpeed = 3.5f;
	public float maxSpeedChange = 0.5f;
	public float wanderAmount = 1;

	private Animator anim;
	private Rigidbody body;
	private Transform target;

	Vector3 targetVelocity;
	Vector3 targetFacing;


	//Vector3 heading = (target.position - transform.position).normalized;
	//Vector3 targetVelocity = heading * maxSpeed;
	//		Vector3 heading = body.velocity.normalized;


	void Start () {
		anim = GetComponent<Animator> ();
		body = GetComponent<Rigidbody> ();
		target = GameObject.FindWithTag ("Player").transform;
		StartCoroutine ("WanderCoroutine");
	}

	IEnumerator WanderCoroutine(){
		bool seesTarget = false;
		while (!seesTarget) {
			targetVelocity = transform.forward * maxSpeed + Random.insideUnitSphere * wanderAmount;
			targetFacing = targetVelocity.normalized;
			yield return new WaitForSeconds (0.5f);
		}
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

		float turnSpeed = Vector3.Cross (transform.forward, targetFacing).y * maxTurnSpeed;
		body.angularVelocity = Vector3.up * turnSpeed;
	}
}

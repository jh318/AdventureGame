using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTricks : MonoBehaviour {

	public Transform playerTransform;
	public float epicCircleDistance = 3;
	public float epicCircleSpeed = 5;
	public float epicCircleHeight = 1;
	public Vector3 epicCircleLookOffset;

	float transitionTime;
	Transform targetTransform;

	private bool gameStarted = false;

	IEnumerator Start () {
		while (!gameStarted) {
			EpicCameraCircle ();
			yield return new WaitForEndOfFrame ();
			gameStarted = Input.anyKey;
		}
		StartCoroutine ("CameraLerp");
	}

	public void Move (Transform targetTransform, float transitionTime) {
		this.transitionTime = transitionTime;
		this.targetTransform = targetTransform;
		if (gameStarted) {
			StopCoroutine ("CameraLerp");
			StartCoroutine ("CameraLerp");
		}
	}

	IEnumerator CameraLerp(){
		for (float t = 0; t < transitionTime; t+=Time.deltaTime) {
			float frac = t / transitionTime;
			transform.position = Vector3.Lerp (transform.position, targetTransform.position, frac);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetTransform.rotation, frac);
			yield return new WaitForEndOfFrame ();
		}
	}

	public void EpicCameraCircle(){
		transform.LookAt(playerTransform.position + epicCircleLookOffset);
		float t = Time.time * epicCircleSpeed;
		transform.position = playerTransform.position + new Vector3 (Mathf.Sin (t), epicCircleHeight, Mathf.Cos (t)) * epicCircleDistance;
	}
}

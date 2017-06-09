using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTricks : MonoBehaviour {

	float transitionTime;
	Transform targetTransform;

	public void Move (Transform targetTransform, float transitionTime) {
		this.transitionTime = transitionTime;
		this.targetTransform = targetTransform;
		StopCoroutine ("CameraLerp");
		StartCoroutine ("CameraLerp");
	}

	IEnumerator CameraLerp(){
		for (float t = 0; t < transitionTime; t+=Time.deltaTime) {
			float frac = t / transitionTime;
			transform.position = Vector3.Lerp (transform.position, targetTransform.position, frac);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetTransform.rotation, frac);
			yield return new WaitForEndOfFrame ();
		}
	}
}

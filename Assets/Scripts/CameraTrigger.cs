using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

	public Transform cameraTransform;
	public float fieldOfView;
	public float transitionTime = 3.0f;
	//public float viewportRectX;
	//public float viewportRectY;
	//public float viewportRectW;
	//public float viewportRectH;

	private CameraIsoController mainCameraScript;
	private Camera cameraComponent;

	void Start(){
		mainCameraScript = Camera.main.GetComponent<CameraIsoController> ();
		cameraComponent = Camera.main.GetComponent<Camera> ();
	}

	void OnTriggerEnter(Collider c)
	{
		Debug.Log ("CameraTrigger");
		if (c.tag == "Player") {
			Debug.Log ("Camera Switch");
			mainCameraScript.enabled = false;
			StartCoroutine ("CameraLerp");
			//Camera.main.transform.position = cameraTransform.position;
			//Camera.main.transform.rotation = cameraTransform.rotation;
			cameraComponent.fieldOfView = fieldOfView;
			//cameraComponent.rect.x = viewportRectX;
			//cameraComponent.rect.y = viewportRectY;
			//cameraComponent.rect.width = viewPort
		}
	}

	IEnumerator CameraLerp(){
		for (float t = 0; t < transitionTime; t+=Time.deltaTime) {
			float frac = t / transitionTime;
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, cameraTransform.position, frac);
			Camera.main.transform.rotation = Quaternion.Slerp (Camera.main.transform.rotation, cameraTransform.rotation, frac);
			yield return new WaitForEndOfFrame ();
		}
	}
}

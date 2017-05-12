using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	Animator animator;
	bool isColliding;

	void Start()
	{
		
		animator = GetComponent<Animator> ();
	}
		
	void OnTriggerEnter()
	{
		isColliding = true;
	}

	void OnTriggerExit()
	{
		isColliding = false;
	}

	void Update()
	{
		OpenDoor ();
	}
		

	void OpenDoor()
	{
		if (isColliding && Input.GetKeyDown (KeyCode.X))
		{
			animator.SetTrigger ("OpenDoorTrigger");
		}
	}

	void CloseDoor()
	{
		transform.rotation = Quaternion.AngleAxis (90, Vector3.left);
	}
}

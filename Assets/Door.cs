using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour {

	Animator animator;
	bool isColliding;

	void Start()
	{
		
		animator = GetComponentInChildren<Animator> ();
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
		ToggleDoor ();
	}
		

	void ToggleDoor()
	{
		if (isColliding && Input.GetKeyDown (KeyCode.X))
		{
			animator.SetTrigger ("ToggleDoor");
		}
	}
}

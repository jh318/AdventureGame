using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour {

	public Text textBox;
	public Text promptBox;

	void Start()
	{
		textBox.gameObject.SetActive(false);
		promptBox.gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.GetComponent<PlayerController>())
		{
			Debug.Log ("Sign Works");
			promptBox.gameObject.SetActive(true);
		}
	}
		
	void OnTriggerExit(Collider c)
	{
		textBox.gameObject.SetActive(false);
		textBox.gameObject.SetActive(false);
	}

	void Update()
	{
		SignInputCheck ();
	}



	void SignInputCheck()
	{
		if (promptBox.gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.X)) 
		{
			textBox.gameObject.SetActive (true);
			promptBox.gameObject.SetActive (false);
		}

		else if (textBox.gameObject.activeSelf == true && Input.GetKeyDown (KeyCode.X)) 
		{
			textBox.gameObject.SetActive (false);
			promptBox.gameObject.SetActive (true);
		}
	}
}

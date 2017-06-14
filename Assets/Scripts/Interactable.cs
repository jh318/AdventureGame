using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

	public Text textBox;
	public Text promptBox;

	public string textBoxString;
	public string promptBoxString;

	bool displayText = false;
	bool displayPrompt = false;

	void Start()
	{
		textBox = GameManager.instance.GetComponent<TextManager> ().textBox;
		promptBox = GameManager.instance.GetComponent<TextManager>().promptBox;
		textBox.gameObject.SetActive(false);
		promptBox.gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.GetComponent<PlayerController>())
		{
			promptBox.text = promptBoxString;
			promptBox.gameObject.SetActive(true);
			displayPrompt = true;
		}
	}

	void OnTriggerExit(Collider c)
	{
		Debug.Log ("On Trigger Exit works");
		promptBox.gameObject.SetActive(false);
		textBox.gameObject.SetActive(false);
		displayPrompt = false;
		displayText = false;
	}

	void Update()
	{
		InteractableInputCheck ();
	}
		
	void InteractableInputCheck()
	{
		if (displayPrompt && (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("ActionButton"))) 
		{
			Debug.Log ("A");
			displayText = true;
			displayPrompt = false;
			textBox.text = textBoxString;
			promptBox.gameObject.SetActive (false);
			textBox.gameObject.SetActive (true);
		}

		else if (displayText && (Input.GetKeyDown (KeyCode.X) || Input.GetButtonDown("ActionButton"))) 
		{
			Debug.Log ("B");
			displayPrompt = true;
			displayText = false;
			textBox.gameObject.SetActive (false);
			promptBox.gameObject.SetActive (true);
		}
	}
}

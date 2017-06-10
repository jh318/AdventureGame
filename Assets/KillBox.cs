using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillBox : MonoBehaviour {

	public static KillBox instance;

	public string deathMessage = "Game Over";
	public Text textBox;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	void Start(){
		textBox = TextManager.instance.textBox;
	}

	void OnTriggerEnter(Collider c){
		if (c.tag == "Player") {
			c.gameObject.SetActive (false);
			textBox.text = deathMessage;
			textBox.gameObject.SetActive (true);
		}
	}

	public void PlayerHasDied(){
		textBox.text = deathMessage;
		textBox.gameObject.SetActive (true);
	}
}

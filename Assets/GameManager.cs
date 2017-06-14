using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	private bool gameStart = false;
	public bool GameStart{
		get {return gameStart; }
		set{gameStart = value; }
	}

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	void Start(){
		gameStart = false;
	}

	public void RestartLevel(){
		if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("RestartButton")){
			StartCoroutine ("RestartLevelCoroutine");
		}
	}

	IEnumerator RestartLevelCoroutine () {
		SceneManager.LoadScene ("animationScene1");
		gameStart = false;
		yield return new WaitForEndOfFrame ();
		yield return new WaitForEndOfFrame ();
		TextManager.instance.textBox.gameObject.SetActive (true);
	}
}

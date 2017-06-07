using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootController : MonoBehaviour {

	public string projectileName;
	public int spreadShotBullets;


	void Start(){
		//TripleShot ();
		//SpreadShot(spreadShotBullets);
		StartCoroutine(SinShot(10));
	}

	void TripleShot(){
		int count = 3;
		float speed = 3.0f;
		for (int i = 0; i < count; ++i) {
			float frac = (float)i / (float)(count - 1);
			float angle = Mathf.Lerp (-45.0f, 45.0f, frac);
			GameObject bullet = Spawner.Spawn(projectileName);
			bullet.transform.position = transform.position;
			bullet.transform.forward = Quaternion.AngleAxis (angle, Vector3.up) * transform.forward;
			//Rigidbody body = bullet.GetComponent<rigidbody> ();
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;
		}
	}

	public void SpreadShot(int numberOfBullets){
		float speed = 3.0f;
		for (int i = 0; i < numberOfBullets; ++i) {
			float frac = (float)i / (float)(numberOfBullets - 1);
			float angle = Mathf.Lerp (-45.0f, 45.0f, frac);
			GameObject bullet = Spawner.Spawn(projectileName);
			bullet.transform.position = transform.position;
			bullet.transform.forward = Quaternion.AngleAxis (angle, Vector3.up) * transform.forward;
			//Rigidbody body = bullet.GetComponent<rigidbody> ();
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;
		}
	}

	public IEnumerator SinShot(int numberOfBullets){
		float speed = 3.0f;
		for (int i = 0; i < numberOfBullets; ++i) {
			//0 for any pi multiple
			//1 for any multiple of pi/2
			//-1 for multiple of 
			GameObject bullet = Spawner.Spawn(projectileName);
			bullet.transform.forward = transform.forward;
			bullet.transform.position = transform.position + transform.right * Mathf.Sin(i * Mathf.PI * 0.25f);
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * speed;
			yield return new WaitForSeconds (0.1f);
		}
	}

	

}

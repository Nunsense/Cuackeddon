using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float speed = 2f;
	private Road road;
	private Vector3 goTarget,goTarget2;
	private bool isGoing;
	private Rigidbody body;

	void Awake() {
		road = FindObjectOfType<Road>();
		body = GetComponent<Rigidbody>();
	}

	void Update() {
		if (isGoing) {
			Vector3 pos = Vector3.Lerp(transform.position, goTarget2, Time.deltaTime * speed);
			//pos.y = transform.localScale.y/2;
			if (Vector3.Distance (transform.position, goTarget2) < 0.5f) {
				print ("gototarget");
				goTarget2 = goTarget;
			}
			transform.position = pos;
		}
	}

	public void GoTo(Vector3 target) {
		if (isGoing)
			return;
		goTarget2 = transform.position;
		goTarget2.x = goTarget2.x - 0.3f;
		if (goTarget.z < transform.position.z) {
			if (transform.position.z > 2) {
				goTarget2.z = transform.position.z - 1.2f;
			} else {
				goTarget2.z = transform.position.z + 1.2f;
			}
		} else {
			if (transform.position.z > 8) {
				goTarget2.z = transform.position.z - 1.2f;
			} else {
				goTarget2.z = transform.position.z + 1.2f;
			}
		}
		goTarget = target;
		isGoing = true;  
  
	}

	void OnCollisionEnter(Collision col) {
		print("Collision: " + col.gameObject.tag);
		if (isGoing) {
			if (col.gameObject.tag == "Section") {
				road.NextFloor ();
			} else {
				if (col.gameObject.tag == "Obstacle") {
					isGoing = false;
				}
			}
		}
	}
}
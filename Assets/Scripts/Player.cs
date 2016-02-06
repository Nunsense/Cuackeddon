using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	private float speed = 2f;
	private Vector3 goTarget, goTarget2;
	private bool isMoving;
	private PathFinder pathFinder;
	private List<Vector3> steps;

	void Awake() {
		pathFinder = GetComponent<PathFinder>();
	}

	void Update() {
		if (isMoving) {
			Vector3 pos = Vector3.Lerp(transform.position, steps[0], Time.deltaTime * speed);
			if (Vector3.Distance(transform.position, steps[0]) < 0.5f) {
				steps.RemoveAt(0);
				isMoving = steps.Count > 0;
			}
			transform.position = pos;
		}
	}

	public void GoTo(Vector3 target) {
		if (isMoving)
			return;

		steps = pathFinder.PathTo(target);
		isMoving = steps.Count > 0; 
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Obstacle") {
			isMoving = false;
		}
	}
}
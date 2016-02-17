using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public float speed = 1.5f;
	private Vector3 goTarget, goTarget2;
	private bool isMoving;
	private PathFinder pathFinder;
	private List<Vector3> steps;
	private Animation anim; 

	void Awake() {
		pathFinder = GetComponent<PathFinder>();
		anim = GameObject.FindGameObjectWithTag("m01_fps_000_h").GetComponent<Animation>();
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
		anim.wrapMode = WrapMode.Loop;
		anim.Play ("Move01_F");
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Obstacle") {
			isMoving = false;
			anim.Stop ();
		}
	}
}
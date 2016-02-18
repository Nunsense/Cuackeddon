using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public float speed = 2f;
	public GameObject walkTargetBeacon;

	private Road road;
	private Vector3 goTarget;
	private bool isGoing;
	private PathFinder pathFinder;
	private List<Node> steps;
	private Animation anim;
	private Rigidbody body;
	private float timeToNextStep = 0f;
	private float timeToNextStepSpent = 0f;

	void Awake() {
		pathFinder = GetComponent<PathFinder>();
		anim = GameObject.FindGameObjectWithTag("m01_fps_000_h").GetComponent<Animation>();
		road = FindObjectOfType<Road>();
		body = GetComponent<Rigidbody>();
		walkTargetBeacon.SetActive(false);
	}

	void Update() {
		if (isGoing) {
			timeToNextStepSpent += Time.deltaTime;
			Vector3 pos = Vector3.Lerp(transform.position, steps[0].position, timeToNextStepSpent / timeToNextStep);
			transform.position = pos;

			if (Vector3.Distance(transform.position, steps[0].position) < 0.5f) {
				steps.RemoveAt(0);
				isGoing = steps.Count > 0;
				if (isGoing)
					SetWalkTime();
				else
					walkTargetBeacon.SetActive(false);
			}
		}

		if (Input.GetMouseButtonDown(0)) { 
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			if (Physics.Raycast(ray, out hit, 1000.0f)) {
				GoTo(hit.point);
			}
		}
	}

	public void GoTo(Vector3 target) {
		isGoing = false;
		steps = pathFinder.PathTo(target);
		if (steps.Count > 0) {
			isGoing = true;

			anim.wrapMode = WrapMode.Loop;
			anim.Play("Move01_F");

			SetWalkTime();

			goTarget = target;
			walkTargetBeacon.transform.position = goTarget;
			walkTargetBeacon.SetActive(true);
		}

//
//		goTarget = transform.position;
//		goTarget.x -= - 0.3f;
//		if (goTarget.z < transform.position.z) {
//			if (transform.position.z > 2) {
//				goTarget.z = transform.position.z - 1.2f;
//			} else {
//				goTarget.z = transform.position.z + 1.2f;
//			}
//		} else {
//			if (transform.position.z > 8) {
//				goTarget.z = transform.position.z - 1.2f;
//			} else {
//				goTarget.z = transform.position.z + 1.2f;
//			}
//		}
	}

	void SetWalkTime() {
		timeToNextStepSpent = 0;
		timeToNextStep = steps[0].distance / speed;
	}

	void OnCollisionEnter(Collision col) {
		if (isGoing) {
			if (col.gameObject.tag == "Obstacle") {
				isGoing = false;
				anim.Stop();
				walkTargetBeacon.SetActive(false);
			}
		}
	}
}
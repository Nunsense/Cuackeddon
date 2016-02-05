using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	public float moveTime = 2f;
	private float currentTime;
	private Vector3 movTarget;
	private bool isMoving = false;

	public void MoveX(float dx) {
		movTarget = transform.position;
		movTarget.x += dx;
		isMoving = true;
		currentTime = 0;
	}

	void Update () {
		if (isMoving) {
			currentTime += Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, movTarget, currentTime / moveTime);

			if (currentTime >= moveTime) {
				isMoving = false;
			}
		}
	}
}

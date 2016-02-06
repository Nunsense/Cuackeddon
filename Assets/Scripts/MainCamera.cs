using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	public Transform target;

	private Vector3 posOffset;

	void Start ()
	{
		posOffset = target.position - transform.position;
	}

	void Update ()
	{
		transform.position = Vector3.Lerp (transform.position, target.position - posOffset, Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.position - transform.position), Time.deltaTime);
	}
}

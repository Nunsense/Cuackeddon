using UnityEngine;
using System.Collections;

public class Duck : MonoBehaviour {

	public Transform player;
	public float distance = 250;
	public float vertical_position = -10;
	// Use this for initialization
	void Start () {
		transform.position = player.position;

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.z = player.position.z + distance;
		pos.y = vertical_position;
		transform.position = pos;
	}
}

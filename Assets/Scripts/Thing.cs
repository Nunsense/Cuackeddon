using UnityEngine;
using System.Collections;

public class Thing : MonoBehaviour {

	private Road road;

	// Use this for initialization
	void Start () {
		road = FindObjectOfType<Road>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		road.goToStuff(transform);
	}
}

﻿using UnityEngine;
using System.Collections;

public class Thing : MonoBehaviour {

	private Road road;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		road = FindObjectOfType<Road>();
	}

	void OnMouseDown() {
		print ("click");
		road.goToStuff(transform);
	}
}
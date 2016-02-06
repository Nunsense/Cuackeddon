using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {
	class Node {
		public Node(float distance, Vector3 position) {
			this.distance = distance;
			this.position = position;
		}

		public float distance;
		public Vector3 position;
	}

	public float stepLenght = 0.5f;

	Vector3[] testSteps = new Vector3[] { 
		new Vector3(0, 0, -1),
		new Vector3(0, 0, 1),
		new Vector3(1, 0, -1),
		new Vector3(1, 0, 0), 
		new Vector3(1, 0, 1),
		new Vector3(-1, 0, -1),
		new Vector3(-1, 0, 0),
		new Vector3(-1, 0, 1)
	};

	public List<Vector3> PathTo(Vector3 target) {
		List<Vector3> steps = new List<Vector3>();

		FindPath(target, transform.position, steps, 0);

		return steps;
	}

	bool FindPath(Vector3 target, Vector3 current, List<Vector3> steps, int n) {
		if (n > 100)
			return false;
		if (Vector3.Distance(current, target) < 1f) {
			return true;
		} else {
			List<Node> nodes = new List<Node>();

			for (int i = 0; i < testSteps.Length; i++) {
				Vector3 nextStep = current + testSteps[i] * stepLenght;

				Ray ray = new Ray(nextStep + Vector3.up, Vector3.down);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 1)) {
					Debug.DrawRay(nextStep, Vector3.up, Color.red, 30);
				} else {
					nodes.Add(new Node(Vector3.Distance(nextStep, target), nextStep));
					Debug.DrawRay(nextStep, Vector3.up, Color.green, 30);
				}
			}

			nodes.Sort(delegate(Node a, Node b) {
				return a.distance.CompareTo(b.distance);
			});

			for (int i = 0; i < nodes.Count; i++) {
				bool reachedTarget = FindPath(target, nodes[i].position, steps, n + 1);
				if (reachedTarget)
					steps.Add(target);
			
				return reachedTarget;
			}
		}
		return false;
	}
}

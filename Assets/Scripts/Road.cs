using UnityEngine;
using System.Collections;

public class Road : MonoBehaviour {

	public GameObject[] floorPrefavs;

	public Player player;

	public int visibleFloors = 5;
	public float floorLenght = 10;
	private float changeFloorDistance;
	private float nextFloorZ;

	private GameObject[] floors;

	private int nextFloorIndex;

	void Awake() {
		player.transform.position = new Vector3(0, 0.61f, 0);
		changeFloorDistance = floorLenght * 2;
		nextFloorZ = 0;
	}

	void Start() {
		nextFloorIndex = 0;

		floors = new GameObject[visibleFloors];
		for (int i = 0; i < visibleFloors; i++) {
			GameObject floor = GameObject.Instantiate(floorPrefavs[Random.Range(0, floorPrefavs.Length)]);
			floors[i] = floor;
			floor.transform.parent = transform;
			floor.transform.localPosition = new Vector3(0, 0, nextFloorZ);
			nextFloorZ += floorLenght; 
		}
	}

	void Update() {
		if (player.transform.position.z >= changeFloorDistance) {
			changeFloorDistance += floorLenght;

			NextFloor();
		}
	}

	public void NextFloor() {
		floors[nextFloorIndex].transform.localPosition = new Vector3(0, 0, nextFloorZ);
		nextFloorIndex = fixIndex(nextFloorIndex + 1);
		nextFloorZ += floorLenght;
	}

	private int fixIndex(int i) {
		return i % floors.Length;
	}
}

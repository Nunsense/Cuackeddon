using UnityEngine;
using System.Collections;

public class Road : MonoBehaviour {

	public GameObject[] floorPrefavs;

	public Player player;

	public int visibleFloors = 5;
	public float floorL = 10;
	public float halfFloorL;

	private GameObject[] floors;

	private int currentFloor;
	private MainCamera cam;

	void Awake() {
		halfFloorL = floorL / 2;
		player.transform.position = new Vector3 (0,0.61f,0);
		cam = FindObjectOfType<MainCamera>();
	}

	void Start() {
		currentFloor = 0;

		floors = new GameObject[visibleFloors];
		for (int i = 0; i < visibleFloors; i++) {
			GameObject floor = GameObject.Instantiate(floorPrefavs[Random.Range(0, floorPrefavs.Length)]);
			floors[i] = floor;
			floor.transform.parent = transform;
			floor.transform.localPosition = new Vector3(i * floorL, 0, 0);
		}
	}

	void Update() {
	}

	public void goToStuff(Transform trans) {
		if (Vector3.Distance(player.gameObject.transform.position, trans.position) < 15) {
			player.GoTo(trans.position);
		}		
	}

	public void NextFloor() {
		currentFloor++;
		//cam.MoveX(floorL * currentFloor);
		floors[fixIndex(currentFloor)].transform.localPosition = new Vector3((currentFloor) * floorL, 0, 0);
	}

	private int fixIndex(int i) {
		Debug.Log(i);
		if (i < 0) i = floors.Length - i;
		if (i > floors.Length - 1) i = i - (floors.Length - 1);
		//Debug.Log(i);
		return i;
	}
}

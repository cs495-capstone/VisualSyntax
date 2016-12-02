using UnityEngine;
using System.Collections;

public class SortingPanels : MonoBehaviour {

	SortingPanel[] panels;

	// Use this for initialization
	void Start () {
		panels = new SortingPanel[5];

		for (int i = 1; i <= panels.Length; i++) {
			panels [i - 1] = GameObject.Find ("CubeSnap" + i).GetComponent<SortingPanel> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			Swap (0, 2);
		}
	}

	public void Swap(int a, int b) {
		GameObject objA = panels [a].connectedObject;
		GameObject objB = panels [b].connectedObject;

		Vector3 tmp = objA.transform.position;
		objA.transform.position = objB.transform.position;
		objB.transform.position = tmp;
	}
}

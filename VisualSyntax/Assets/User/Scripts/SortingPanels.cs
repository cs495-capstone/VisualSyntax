using UnityEngine;
using System.Collections;

public class SortingPanels : MonoBehaviour, ISortListener {

	SortingPanel[] panels;
	SortManager manager;
	bool first;

	int snappedCount = 0;

	// Use this for initialization
	void Start () {
		panels = new SortingPanel[5];
		for (int i = 1; i <= panels.Length; i++) {
			panels [i - 1] = GameObject.Find ("CubeSnap" + i).GetComponent<SortingPanel> ();
			panels [i - 1].AddSortListener (this);
		}
		this.first = true;
	}

	// Update is called once per frame
	void Update () {
		if (snappedCount >= 5) {
			if (this.first) {
				this.manager = new SortManager (panels, new SelectionSort ());
				this.first = false;
			}
			this.manager.Update ();
		}
	}

	public void Swap(int a, int b) {
		GameObject objA = panels [a].connectedObject;
		GameObject objB = panels [b].connectedObject;

		Vector3 tmp = objA.transform.position;
		objA.transform.position = objB.transform.position;
		objB.transform.position = tmp;
	}

	public void OnSnap() {
		snappedCount++;
		Debug.Log (snappedCount);
	}

	public void OnUnsnap() {
		snappedCount--;
		Debug.Log (snappedCount);
	}
}

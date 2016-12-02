using UnityEngine;
using System.Collections;
using VRTK;

public class SortingPanel : MonoBehaviour {

	public GameObject connectedObject;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public int GetValue() {
		return 0;
	}
		
	public void ObjectConnected(object obj, SnapDropZoneEventArgs args) {
		connectedObject = args.snappedObject;
	}

	public void ObjectDisconnected(object obj, SnapDropZoneEventArgs args) {
		connectedObject = null;
	}
}

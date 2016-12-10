using UnityEngine;
using System.Collections;
using VRTK;

public class SortingPanel : MonoBehaviour {

	public GameObject connectedObject;

	public ArrayList listeners = new ArrayList();

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public int GetValue() {
		return connectedObject == null ? -1 : int.Parse (connectedObject.GetComponent<VroomObject> ().Label);
	}
		
	public void ObjectConnected(object obj, SnapDropZoneEventArgs args) {
		connectedObject = args.snappedObject;

		foreach (var listener in listeners) {
			((ISortListener)listener).OnSnap ();
		}
	}

	public void ObjectDisconnected(object obj, SnapDropZoneEventArgs args) {
		connectedObject = null;

		foreach (var listener in listeners) {
			((ISortListener)listener).OnUnsnap ();
		}
	}

	public void AddSortListener(ISortListener listener) {
		listeners.Add (listener);
	}
}

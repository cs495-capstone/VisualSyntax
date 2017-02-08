using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

/// <summary>
/// This class is used for the list sorting prefab found in the unity world
/// </summary>
public class SortingPanels : MonoBehaviour, ISortListener {

	public readonly string MSG_INITIALIZED = "INITIALIZED";

	public readonly string MSG_UNITIALIZED = "UNINITIALIZED";

	/// <summary>
	/// This is the number of panels that will be created by default
	/// </summary>
	int numPanels = 5;

	/// <summary>
	/// This is a list of the panels used to check list sorting.
	/// </summary>
	SortingPanel[] panels;

	/// <summary>
	/// This counts the number of items currently "snapped" onto a panel.
	/// </summary>
	int snappedCount = 0;

	///<summary>
	/// This is the list of listeners waiting for all blocks to be on the panels
	/// </summary>
	List<IEventListener> listeners = new List<IEventListener>();

	public int Count { get { return numPanels; } }

	/// <summary>
	/// This is a method used to initialize the class
	/// </summary>
	void Start () {
		//Right now we setup 5 panels
		panels = GetComponentsInChildren<SortingPanel>();
		foreach (var panel in panels) {
			panel.AddSortListener (this);
		}
	}

	/// <summary>
	/// This method is called once per frame, and we check our panel status 
	/// each frame
	/// </summary>
	void Update () {
	}

	/// <summary>
	/// Sends the event to each listener of this object.
	/// </summary>
	/// <param name="message">The message to send to every listener.</param>
	private void Broadcast(string message) {
		foreach (var listener in listeners) {
			listener.OnMessageReceived (this, new GameEventArgs (){ Message = message });
		}
	}

	/// <summary>
	/// Subscribe the specified subscriber.
	/// </summary>
	/// <param name="subscriber">Subscriber.</param>
	public void Subscribe(IEventListener subscriber) { 
		listeners.Add (subscriber);
	}

	/// <summary>
	/// This method swaps two cubes on the different panels.
	/// </summary>
	/// <param name="a">The index a to swap</param>
	/// <param name="b">The index b to swap</param>
	public void Swap(int a, int b) {
		GameObject objA = panels [a].connectedObject;
		GameObject objB = panels [b].connectedObject;

		Vector3 tmp = objA.transform.position;
		objA.transform.position = objB.transform.position;
		objB.transform.position = tmp;
	}

	/// <summary>
	/// This method is called when a cube is snapped onto a panel.
	/// </summary>
	public void OnSnap() {
		snappedCount++;

		if (snappedCount == panels.Length) {
			Broadcast (MSG_INITIALIZED);
		}
		Debug.Log (snappedCount);
	}

	/// <summary>
	/// This method is called when a cube is unsnapped from a panel.
	/// </summary>
	public void OnUnsnap() {
		snappedCount--;
		if (snappedCount != panels.Length) {
			Broadcast (MSG_UNITIALIZED);
		}
		Debug.Log (snappedCount);
	}

	public int[] GetValues() {
		var result = new int[panels.Length];

		for (int i = 0; i < panels.Length; i++) {
			result [i] = panels [i].GetValue ();
		}

		return result;
	}

	public void InitializePanels(int[] values) {
		for (int i = 0; i < values.Length; i++) {
			var panel = panels [i];
			panel.connectedObject.GetComponent<VroomObject> ().SetLabel ("" + values [i]);
		}
	}

	public SortingPanel[] GetPanels() {
		return panels;
	}
}

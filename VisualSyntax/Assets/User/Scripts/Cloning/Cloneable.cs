using UnityEngine;
using System.Collections.Generic;

public class Cloneable : MonoBehaviour {

	//This is the list of shallow clones that are listening to the original
	private List<IEventListener> cloneListeners = new List<IEventListener>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Clone(bool shallow) {
		Debug.Log ("Cloned");
		var newKey = GameObject.Instantiate (this.gameObject);
		var shallowClone = newKey.AddComponent<ShallowClone> ();
		newKey.transform.position = this.transform.position;
		//newKey.GetComponent<Cloneable>().enabled = false;
		if (shallow) {
			Subscribe (shallowClone);
		}
	}

	public void Subscribe(IEventListener listener) {
		cloneListeners.Add (listener);
	}

	public void Broadcast(GameObject newKey) {
		foreach (var listener in cloneListeners) {
			listener.OnMessageReceived (this, new CloneEventArgs (){ NewKey = newKey });
		}
	}
}

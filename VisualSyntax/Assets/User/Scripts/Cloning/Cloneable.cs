using UnityEngine;
using System.Collections.Generic;
using System;

public class Cloneable : MonoBehaviour, IEventListener {

	private StaticType staticType { get { return GetComponent<StaticType>(); }}

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

		// Clone the static type
		var newObject = GameObject.Instantiate (this.gameObject);
		var newStaticType = newObject.GetComponent<StaticType> ();

		// Disable the ability to clone clones
		newObject.GetComponent<Cloneable> ().enabled = false;

		// Clone the dynamic type
		var keyObject = staticType.GetConnectedObject ();
		GameObject newKeyObject;
		if (keyObject != null) {
			newKeyObject = GameObject.Instantiate (keyObject);

			// Attach dynamic type to static type
			newStaticType.ConnectObject(newKeyObject.GetComponent<Rigidbody>());
		}



		// If shallow cloning, subscribe to shallow clone changes
		if (shallow) {
			var shallowClone = newObject.AddComponent<ShallowClone> ();
			Subscribe (shallowClone);
			shallowClone.Subscribe (this);
		}
	}

	public void Subscribe(IEventListener listener) {
		cloneListeners.Add (listener);
	}

	public void Broadcast(GameObject newKey) {
		foreach (var listener in cloneListeners) {
			Debug.Log ("Parent changed!");
			listener.OnMessageReceived (this, new CloneEventArgs (){ NewKey = newKey });
		}
	}

	public void OnMessageReceived(object sender, EventArgs ea) {
		var cloneArgs = (CloneEventArgs)ea;

		// Change the dynamic type out
		var dynamicTypeObj = GetComponentInChildren<DynamicType>().gameObject;

		// Clone the new dynamic type and attach to this object
		var newKey = GameObject.Instantiate(cloneArgs.NewKey);

		// Set the position of the new key to be exactly where it should.
		newKey.transform.position = dynamicTypeObj.transform.position;

		// Set the parent of the new key to be this clone
		newKey.transform.parent = this.transform;

		// Delete the old dynamic type
		GameObject.Destroy (dynamicTypeObj);
	}
}

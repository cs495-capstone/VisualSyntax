using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// This class is what give the functionality for an object to be cloned.  In our interfaces 
/// implementation there is a static type that is cloned, and the dynamic type
/// will be cloned as well.
/// </summary>
public class Cloneable : MonoBehaviour, IEventListener {

	/// <summary>
	/// This is the static type object that will act as the parent object for cloning
	/// </summary>
	/// <value>StaticType is the class representing the static type in an interfacing relationship.</value>
	private StaticType staticType { get { return GetComponent<StaticType>(); }}

	/// <summary>
	/// This is the list of shallow clones that are listening to the original
	/// </summary>
	private List<IEventListener> cloneListeners = new List<IEventListener>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// This is the method responsible for cloning the clonable object.  It will instantiate a new
	/// instance of the game object and it will also clone and snap in the dynamic type assiociated
	/// with the clonable object.
	/// </summary>
	/// <param name="shallow">If set to <c>true</c> the clone will be a shallow clone
	/// 					  and have a relationship with its parent clone.</param>
	public void Clone(bool shallow) {
		Debug.Log ("Cloned");


			// Clone the static type
			var newObject = GameObject.Instantiate (this.gameObject);
			var newStaticType = newObject.GetComponent<StaticType> ();

			// newObject.GetComponentInChildren<VRTK.VRTK_SnapDropZone> ().snapType = VRTK.VRTK_SnapDropZone.SnapTypes.Use_Parenting;


			// Clone the dynamic type
			var keyObject = staticType.GetConnectedObject ();
			GameObject newKeyObject;
			if (keyObject != null) {
				newKeyObject = GameObject.Instantiate (keyObject);

				// Attach dynamic type to static type
				newStaticType.ConnectObject (newKeyObject.GetComponent<Rigidbody> ());
			}



			// If shallow cloning, subscribe to shallow clone changes
			if (shallow) {
				newObject.GetComponent<MeshRenderer> ().material.color = new Color (255, 0, 0);
				var shallowClone = newObject.AddComponent<ShallowClone> ();
				Subscribe (shallowClone);
				shallowClone.Subscribe (this);

				// Disable the ability to clone clones
				newObject.GetComponent<Cloneable> ().enabled = false;
			}
	
	}

	/// <summary>
	/// The subscribe method is used to register shallow clones to listen to 
	/// the changes of its parent.
	/// </summary>
	/// <param name="listener">Listener.</param>
	public void Subscribe(IEventListener listener) {
		cloneListeners.Add (listener);
	}

	/// <summary>
	/// Since shallow clones should change dynamic types when it's parent does,
	/// we will broadcast the new gameobject each time the key is changed.
	/// </summary>
	/// <param name="newKey">New key object attached to the clonable object.</param>
	public void Broadcast(GameObject newKey) {
		foreach (var listener in cloneListeners) {
			Debug.Log ("Parent changed!");
			listener.OnMessageReceived (this, new CloneEventArgs (){ NewKey = newKey });
		}
	}

	/// <summary>
	/// Changes in the shallow clones will also make a change in the parent,
	/// therefore this method listens for a braodcast and it will change
	/// its dynamic type to the one given in the EventArgs.
	/// </summary>
	/// <param name="sender">The object sending the message.</param>
	/// <param name="ea">The arguments holding the new dynamic type.</param>
	public void OnMessageReceived(object sender, EventArgs ea) {
		Debug.Log ("Parent says: Clone changed.");
		var cloneArgs = (CloneEventArgs)ea;

		// Change the dynamic type out
		var dynamicTypeObj = staticType.GetConnectedObject();

		// Clone the new dynamic type and attach to this object
		var newKey = GameObject.Instantiate(cloneArgs.NewKey);

		// Set the position of the new key to be exactly where it should.
		newKey.transform.rotation = dynamicTypeObj.transform.rotation;
		newKey.transform.position = dynamicTypeObj.transform.position;

		// Update the new key's static type to match this one
		staticType.ConnectObject (newKey.GetComponent<Rigidbody>());
		staticType.Name = newKey.GetComponent<DynamicType>().Name;

		// Delete the old dynamic type
		GameObject.Destroy (dynamicTypeObj);

		Broadcast (newKey);
	}
}

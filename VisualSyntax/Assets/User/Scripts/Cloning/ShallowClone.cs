using UnityEngine;
using System.Collections;

public class ShallowClone : MonoBehaviour, IEventListener
{
	private ArrayList listeners = new ArrayList ();

	private StaticType staticType;
	private DynamicType dynamicType;

	// Use this for initialization
	void Start ()
	{
		listeners = new ArrayList ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (dynamicType == null) {
			dynamicType = GetComponentInChildren<DynamicType> ();
			staticType = GetComponentInChildren<StaticType> ();
		}
	}

	public void Subscribe(IEventListener listener) {
		listeners.Add (listener);
	}

	public void Broadcast() {
		Debug.Log ("Clone changed!");
		foreach (IEventListener listener in listeners) {
			listener.OnMessageReceived (this, new CloneEventArgs () { NewKey = dynamicType.gameObject});
		}
	}

	/// <summary>
	/// This message is received when one of its shallow associates are cloned.
	/// </summary>
	/// <param name="newKey">New key.</param>
	/// <param name="ea">Ea.</param>
	public void OnMessageReceived(object sender, System.EventArgs ea) {
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
		// GameObject.Destroy (dynamicTypeObj);
	}
}


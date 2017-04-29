using UnityEngine;
using System.Collections;

/// <summary>
/// This class holds all the functionality for implementing a
/// shallow clone in Unity.
/// </summary>
public class ShallowClone : MonoBehaviour, IEventListener
{
	/// <summary>
	/// This is the list of objects that are observing
	/// the shallow clone.  In most cases this is only
	/// the parent clone.
	/// </summary>
	private ArrayList listeners = new ArrayList ();

	/// <summary>
	/// This is the static type of the clone that will
	/// hold differente Dynamic Types.
	/// </summary>
	/// <value>StaticType object in the clone.</value>
	private StaticType staticType { get { return GetComponent<StaticType>(); }}

	/// <summary>
	/// This is the dynamic type that the shallow clone contains.
	/// </summary>
	/// <value>The dynamic type object on the static type.</value>
	private DynamicType dynamicType{ get { return 
			staticType.GetConnectedObject() == null ? null : staticType.GetConnectedObject().GetComponent<DynamicType>(); }}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	/// <summary>
	/// This method is used to subscribe observers to this class
	/// </summary>
	/// <param name="listener">The object that will be listening.</param>
	public void Subscribe(IEventListener listener) {
		listeners.Add (listener);
	}

	/// <summary>
	/// This method is used to broadcast when the dynamic type chagnes for the shallow clone.
	/// </summary>
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

		staticType.GetComponentInChildren<VRTK.VRTK_SnapDropZone> ().ForceUnsnap ();
		staticType.GetComponentInChildren<FixedJoint> ().connectedBody = null;

		// Update the new key's static type to match this one
		staticType.ConnectObject (newKey.GetComponent<Rigidbody>());
		staticType.Name = newKey.GetComponent<DynamicType>().Name;

		// Delete the old dynamic type
		GameObject.Destroy (dynamicTypeObj);
	}
}


using UnityEngine;
using System.Collections;
using VRTK;
using System;

/// <summary>
/// This is the class representing a StaticType object when interfacing is used.
/// </summary>
public class StaticType : MonoBehaviour {

	/// <summary>
	/// This is the name of the static type that will be used for ID purposes.
	/// </summary>
	public string Name;

	/// <summary>
	/// This is the dynamic type that is implemented by the static type.
	/// </summary>
	private DynamicType snappedObj;

	/// <summary>
	/// This is the zone that is used to create joints between static types
	/// and dynamic types.
	/// </summary>
	/// <value>The snap drop zone.</value>
	private VRTK_SnapDropZone snapDropZone { get { return GetComponentInChildren<VRTK_SnapDropZone> (); } }

	/// <summary>
	/// This is the joint used to hold the static and dynamic type together.
	/// </summary>
	/// <value>The fixed joint.</value>
	private FixedJoint fixedJoint { get { return snapDropZone.gameObject.GetComponent<FixedJoint> (); } }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}



	/// <summary>
	/// This method is used to let all listeners of the panel
	/// know an object has been snapped.  This is done
	/// by calling the OnSnap of all listeners.
	/// </summary>
	/// <param name="obj">Object that is connected</param>
	/// <param name="args">This is the arguments 
	/// of what was snapped in</param>
	public void ObjectConnected(object obj, SnapDropZoneEventArgs args) {
		snappedObj = args.snappedObject.GetComponent<DynamicType> ();
		Name = snappedObj.Name;

		if (GetComponent<ShallowClone> () != null) {
			GetComponent<ShallowClone> ().Broadcast ();
		} else {
			GetComponent<Cloneable> ().Broadcast (snappedObj.gameObject);
		}
	}

	/// <summary>
	/// This is the method that alerts all listeners when
	/// an object is disconnected from the panel, by
	/// calling the OnUnsnap.
	/// </summary>
	/// <param name="obj">Object that was 
	/// disconnected</param>
	/// <param name="args">These are the arguments of
	/// the object in the snapzone</param>
	public void ObjectDisconnected(object obj, SnapDropZoneEventArgs args) { 
		snappedObj = null;
		Name = "";
	}

	/// <summary>
	/// This method returns the object that is connected to the StaticType's 
	/// joint.
	/// </summary>
	/// <returns>The connected object.</returns>
	public GameObject GetConnectedObject() {
		if (fixedJoint.connectedBody == null)
			return null;
		return fixedJoint.connectedBody.gameObject;
	}

	/// <summary>
	/// This method uses the VRTK library to attach one object to this one.
	/// </summary>
	/// <param name="obj">Object to attach.</param>
	public void ConnectObject(Rigidbody obj) {
		var args = new SnapDropZoneEventArgs ();
		args.snappedObject = obj.gameObject;
		snappedObj = obj.gameObject.GetComponentInChildren<DynamicType>();

		snapDropZone.isSnapped = false;
		snapDropZone.ForceUnsnap ();
		snapDropZone.ForceSnap (obj.gameObject);
		//snapDropZone.GetComponent<VRTK.UnityEventHelper.VRTK_SnapDropZone_UnityEvents> ().OnObjectSnappedToDropZone.Invoke (snapDropZone, args );

		//snapDropZone.ForceSnap (obj.gameObject);
		//snapDropZone.SnapObject(obj.GetComponentInChildren<Collider>());

		var interactableObject = obj.GetComponent<VRTK_InteractableObject> ();
		interactableObject.ToggleSnapDropZone (snapDropZone, true);

		snapDropZone.isSnapped = true;
		snapDropZone.currentSnappedObject = obj.gameObject;

		fixedJoint.connectedBody = obj;
	}

}

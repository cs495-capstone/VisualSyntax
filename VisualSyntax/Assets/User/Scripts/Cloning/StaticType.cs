using UnityEngine;
using System.Collections;
using VRTK;
using System;

public class StaticType : MonoBehaviour {

	public string Name;

	private DynamicType snappedObj;

	private VRTK_SnapDropZone snapDropZone { get { return GetComponentInChildren<VRTK_SnapDropZone> (); } }
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

	public GameObject GetConnectedObject() {
		return fixedJoint.connectedBody.gameObject;
	}

	public void ConnectObject(Rigidbody obj) {
		snapDropZone.ForceUnsnap ();
		//snapDropZone.ForceSnap (obj.gameObject);
		fixedJoint.connectedBody = obj;
	}

}

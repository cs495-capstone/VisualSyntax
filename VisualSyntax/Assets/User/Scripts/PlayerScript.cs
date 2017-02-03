using UnityEngine;
using System.Collections;

/// <summary>
/// This was our first attempt at the VroomObject.
/// IT IS NO LONGER IN USE. It is here for 
/// debug purposes.
/// </summary>
public class PlayerScript : MonoBehaviour
{
	/// <summary>
	/// We check if we have been toggled.
	/// </summary>
	bool toggled;

	/// <summary>
	/// This is our game object JanIce
	/// </summary>
	GameObject janice;

	/// <summary>
	/// At the start Find JanIce
	/// </summary>
	void Start() {
		//Find the JanIce object
		janice = GameObject.Find ("Janice");
	}

	/// <summary>
	/// This method is responsible for turning reference mode on for JanIce
	/// when R is pressed so that we can test reference mode easily
	/// </summary>
	void Update() {
		if (!toggled && Input.GetKeyDown (KeyCode.R)) {
			janice.GetComponent<VroomObject> ().ReferenceMode = !janice.GetComponent<VroomObject> ().ReferenceMode;
			toggled = true;
		} else if (Input.GetKeyUp (KeyCode.R)) {
			toggled = false;
		}
	}
}
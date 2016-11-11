using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	bool toggled;

	GameObject janice;

	void Start() {
		janice = GameObject.Find ("Janice");
	}

	void Update() {
		if (!toggled && Input.GetKeyDown (KeyCode.R)) {
			janice.GetComponent<VroomObject> ().ReferenceMode = !janice.GetComponent<VroomObject> ().ReferenceMode;
			toggled = true;
		} else if (Input.GetKeyUp (KeyCode.R)) {
			toggled = false;
		}
	}
}
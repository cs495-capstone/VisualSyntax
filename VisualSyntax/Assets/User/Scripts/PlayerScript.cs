using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

	bool toggled;

	void Start() {
	}

	void Update() {
		if (!toggled && Input.GetKeyDown (KeyCode.Space)) {
			toggled = true;
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			toggled = false;
		}
	}
}
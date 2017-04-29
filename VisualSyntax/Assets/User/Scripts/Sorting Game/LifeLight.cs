using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

/// <summary>
/// This class manages the light in a room.
/// </summary>
public class LifeLight : MonoBehaviour
{
	/// <summary>
	/// This is the array of connected lights.
	/// </summary>
	private Light[] connectedLights;

	/// <summary>
	/// This method intializes the array of lights in the room.
	/// </summary>
	public void Start() {
		connectedLights = this.GetComponentsInChildren<Light> ();
	}

	/// <summary>
	/// This method turns lights on and off based on the parameter
	/// </summary>
	/// <param name="on">If set to <c>true</c> turn on.</param>
	public void LightStatus(bool on) {
		foreach (Light connectedLight in connectedLights) {
			if (on) {
				connectedLight.intensity = 1.0f;
			} else {
				connectedLight.intensity = 0f;
			}
		}
		if (!on) GetComponentInChildren<AudioSource> ().Play ();
	}
}



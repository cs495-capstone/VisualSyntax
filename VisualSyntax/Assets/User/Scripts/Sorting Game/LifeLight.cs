using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class LifeLight : MonoBehaviour
{
	private Light[] connectedLights;

	public void Start() {
		connectedLights = this.GetComponentsInChildren<Light> ();
	}

	public void LightStatus(bool on) {
		foreach (Light connectedLight in connectedLights) {
			if (on) {
				connectedLight.intensity = 1.0f;
			} else {
				connectedLight.intensity = 0f;
			}
		}
	}
}



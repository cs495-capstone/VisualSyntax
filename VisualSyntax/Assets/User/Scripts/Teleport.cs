using UnityEngine;
using System.Collections;
/// <summary>
/// This class is used to change the position of an object based on the location of an indicator.
/// </summary>
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Teleport : MonoBehaviour {
	/// <summary>
	/// The indicator is an object that holds the location of where a user is looking
	/// </summary>
	public GameObject indicator;

	public GameObject cameraRig;

	SteamVR_TrackedObject trackedObj;

	bool teleporting;

	LineRenderer laserPointer;
	private Vector3 hitPos;


	/// <summary>
	/// This method finds the indicator object in the world and pulls it in.
	/// </summary>
	void Start () {
		laserPointer = GameObject.Find ("LaserPointer").GetComponent<LineRenderer>();
		laserPointer.SetWidth (0.01f, 0.01f);
		if (cameraRig == null) {
			cameraRig = GameObject.Find ("[CameraRig]");
		}
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
		if (indicator == null) {
			indicator = GameObject.Find ("indicator");
		}
		indicator.GetComponent<MeshRenderer> ().enabled = false;
	}

	/// <summary>
	/// This method checks to see if the teleport key is down, and if so the players position
	/// becomes the postiion of the indicator.
	/// </summary>
	void Update () {
		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (!teleporting && device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			teleporting = true;
		}

		if (teleporting) {
			indicator.GetComponent<MeshRenderer> ().enabled = true;
			RaycastHit hit;

			// Shoot a ray out from the camera using its angle and position
			Ray ray = new Ray (transform.position, transform.forward);

			// See if the ray intersects with the world
			if (Physics.Raycast (ray, out hit)) {
				// Only set the indicator if the ray hits terrain. This prevents teleporting into the sky
				if (hit.collider.name == "Terrain" && hitPos != hit.point) {
					hitPos = hit.point;
					indicator.transform.position = hitPos;
				}

				laserPointer.SetPosition (0, transform.position);
				laserPointer.SetPosition (1, indicator.transform.position);
			} else {
				teleporting = false;
				laserPointer.SetPosition (0, Vector3.zero);
				laserPointer.SetPosition (1, Vector3.zero);
				indicator.GetComponent<MeshRenderer> ().enabled = false;
			}
		}

		if (teleporting && device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			var bounds = cameraRig.GetComponent<MeshRenderer> ().bounds;
			SteamVR_Fade.Start (Color.black, 1f);
			cameraRig.transform.position = indicator.transform.position + 
				new Vector3(0, -bounds.size.y / 2, 0);
			SteamVR_Fade.Start (Color.clear, 1f);

			teleporting = false;
			indicator.GetComponent<MeshRenderer> ().enabled = false;
			laserPointer.SetPosition (0, Vector3.zero);
			laserPointer.SetPosition (1, Vector3.zero);
		}


	}
}

using UnityEngine;
using System.Collections;

using NewtonVR;

public class RefGun : MonoBehaviour {

	public GameObject lineRenderer;

	NVRInteractableItem item;
	LineRenderer lineRenderer_comp;

	bool toggled = false;

	// Use this for initialization
	void Start () {
		item = GetComponent<NVRInteractableItem> ();
		lineRenderer_comp = lineRenderer.GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (item.IsAttached && item.AttachedHand.UseButtonPressed) {
			updateLaser (item.AttachedHand);
		} else {
			toggled = false;
			disableLaser ();
		}
	}

	void updateLaser(NVRHand hand) {
		RaycastHit outHit;
		Ray outRay = new Ray (transform.position, transform.forward);

		if (Physics.Raycast (outRay, out outHit)) {
			if (outHit.rigidbody != null && !toggled) {
				var janice = outHit.rigidbody.gameObject.GetComponent<VroomObject> ();
				janice.ReferenceMode = !janice.ReferenceMode;
				toggled = true;
			}

			// Do something
			lineRenderer_comp.SetPosition (0, transform.position);
			lineRenderer_comp.SetPosition (1, outHit.point);
		} else {
			disableLaser ();
		}
	}

	void disableLaser() {
		lineRenderer_comp.SetPosition (0, Vector3.zero);
		lineRenderer_comp.SetPosition (1, Vector3.zero);
	}
}

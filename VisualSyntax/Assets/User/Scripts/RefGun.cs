using UnityEngine;
using System.Collections;

using NewtonVR;

public class RefGun : VRTK.VRTK_InteractableObject {

	public GameObject lineRenderer;

	LineRenderer lineRenderer_comp;

	bool toggled = false;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		lineRenderer_comp = lineRenderer.GetComponent<LineRenderer> ();
	}


	public override void StartUsing(GameObject usingObject)
	{
		base.StartUsing (usingObject);
	}

	protected override void Update() {
		if (base.IsUsing ()) {
			updateLaser ();
		} else {
			disableLaser ();
		}
	}
		

	void updateLaser() {
		Debug.Log ("Using the gun!");
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
		toggled = false;
	}
}

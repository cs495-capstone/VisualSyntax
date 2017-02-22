using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to manage the gun that turns on
/// reference mode.
/// </summary>
public class CloneGun : VRTK.VRTK_InteractableObject {

	/// <summary>
	/// This is used to get the LineRenderer component.
	/// </summary>
	public GameObject lineRenderer;

	/// <summary>
	/// This is the component that is used to create the line.
	/// </summary>
	LineRenderer lineRenderer_comp;

	/// <summary>
	/// This is a boolean that is true when an object was hit
	/// </summary>
	bool toggled = false;

	/// <summary>
	/// If the gun is cooling down
	/// </summary>
	bool coolDown = false;

	float timeElapsed = 0;

	/// <summary>
	/// This method initializes the line renderer compenent
	/// from the lineRenderer.
	/// </summary>
	protected override void Start () {
		base.Start ();
		lineRenderer_comp = lineRenderer.GetComponent<LineRenderer> ();
	}

	/// <summary>
	/// This is called when an object is used initially
	/// </summary>
	/// <param name="usingObject">The game object that is currently 
	/// using this object.</param>
	public override void StartUsing(GameObject usingObject)
	{
		base.StartUsing (usingObject);
	}

	/// <summary>
	/// This mehtod is called once per frame, and if the gun is being
	/// used we update the laser.  Otherwise it is disabled.
	/// </summary>
	protected override void Update() {
		if (!coolDown) {
			if (base.IsUsing ()) {
				updateLaser ();
			} else {
				disableLaser ();
			}
		} else {
			disableLaser ();
			timeElapsed += Time.deltaTime;
			if (timeElapsed >= 0) {
				coolDown = false;
				timeElapsed = 0;
			}
		}
			
	}

	/// <summary>
	/// This is the method responsible for updating the laser.
	/// </summary>
	void updateLaser() {
		//This is for our use in testing
		Debug.Log ("Using the gun!");
		//This is how we are checking if the laser hits an object.
		RaycastHit outHit;
		Ray outRay = new Ray (transform.position, transform.forward);

		//If we get a hit
		if (Physics.Raycast (outRay, out outHit)) {
			//We make sure the hit has a rigid body, and we have not already hit the object
			if (outHit.rigidbody != null && !toggled) {
				//JanIce is the name we originally gave objects containing objects
				var toClone = outHit.rigidbody.gameObject.GetComponent<Cloneable> ();
				if (toClone.enabled) {
					toClone.Clone (false);
					coolDown = true;
				}
			}

			// create a laser beam between our gun and what we are pointing at
			lineRenderer_comp.SetPosition (0, transform.position);
			lineRenderer_comp.SetPosition (1, outHit.point);
			// If we aren't hitting anything then we must be pointing to the sky, so we disable
			// the laser.
		} else {
			disableLaser ();
		}
	}

	// Essentially turn the laser beam off and toggle to false.
	void disableLaser() {
		lineRenderer_comp.SetPosition (0, Vector3.zero);
		lineRenderer_comp.SetPosition (1, Vector3.zero);
		toggled = false;
	}
}

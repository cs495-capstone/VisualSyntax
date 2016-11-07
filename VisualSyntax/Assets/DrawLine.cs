using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

	/// <summary>
	/// This is the origin of the line.
	/// </summary>
	public Transform origin;

	/// <summary>
	/// This is the end-point of the line
	/// </summary>
	public Transform destination;

	private LineRenderer lineRenderer;

	/// <summary>
	/// This method initializes the script. We set the line renderer's origin position
	/// to the specified object and then shrink the size of the line to be manageable in
	/// the world.
	/// </summary>
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetPosition (0, origin.position);
		lineRenderer.SetWidth (0.01f, 0.01f);
	}

	/// <summary>
	/// This update method sets the line render's end-point to the destination transform
	/// specified in the field. This gets updated once per frame that the game is running.
	/// </summary>
	void Update () {
		if (origin != null && destination != null) {
			lineRenderer.SetPosition (1, destination.position);
		}
	}
}

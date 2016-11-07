using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

	public Transform origin;
	public Transform destination;

	private LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetPosition (0, origin.position);
		lineRenderer.SetWidth (0.01f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		if (origin != null && destination != null) {
			lineRenderer.SetPosition (1, destination.position);
		}
	}
}

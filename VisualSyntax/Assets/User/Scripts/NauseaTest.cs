using UnityEngine;
using System.Collections;

/// <summary>
/// This script is one of the iterations of our user
/// comfort testing.  This was a simple rotation of
/// the camera view without the user moving.
/// </summary>
public class NauseaTest : MonoBehaviour {

	/// <summary>
	/// This is used to initialize the script, however
	/// we have nothing to initialize.
	/// </summary>
	void Start () {
	
	}
	
	/// <summary>
	/// Rotate the camera view once per frame.
	/// </summary>
	void Update () {
		transform.Rotate (Vector3.one * 10);
	}
}

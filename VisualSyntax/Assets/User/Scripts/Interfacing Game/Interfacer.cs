using UnityEngine;
using System.Collections;

/// <summary>
/// This is the class that provides code needed for an object to manipulate others.
/// </summary>
public class Interfacer : MonoBehaviour {

	/// <summary>
	/// This is the object that will be tranformed into on implementation
	/// </summary>
	public GameObject transformer;

	/// <summary>
	/// This is the scale that the new object will be recreated in/
	/// </summary>
	public Vector3 scale = Vector3.one;
	public string Name;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// This is the code used to change an object when it collides with this one.
	/// </summary>
	/// <param name="collision">The collision of the object.</param>
	void OnCollisionEnter(Collision collision) {
		var otherObject = collision.gameObject;
		if (otherObject.GetComponent<Interfacable> () != null) {
			var targetMesh = transformer.GetComponent<MeshFilter> ();
			if (targetMesh != null) {
				otherObject.GetComponent<Interfacable> ().Interface (new InterfaceInfo () {
					Name = Name,
					TargetObject = transformer
				});
			}
		}
	}
}

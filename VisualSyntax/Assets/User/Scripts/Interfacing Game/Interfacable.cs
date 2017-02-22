using UnityEngine;
using System.Collections;

public class Interfacable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	/*void OnCollisionEnter(Collision collision) {
		var other = collision.gameObject;
		if (other.GetComponent<Interfacer> () != null) {
			// Transform into the object that the interfacer tells us to
			Transform(other.GetComponent<Interfacer>().GetMesh());
		}
	}*/

	/// <summary>
	/// Transforms this object into a copy of the passed in object
	/// </summary>
	/// <param name="targetObj">Target object.</param>
	public void Interface(Mesh mesh) {
		var meshFilter = GetComponent<MeshFilter> ();
		if (meshFilter != null) {
			meshFilter.mesh = mesh;
		}
	}
}

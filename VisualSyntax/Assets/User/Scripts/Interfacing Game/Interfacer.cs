using UnityEngine;
using System.Collections;

public class Interfacer : MonoBehaviour {

	public GameObject transformer;
	public Vector3 scale = Vector3.one;
	public string Name;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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

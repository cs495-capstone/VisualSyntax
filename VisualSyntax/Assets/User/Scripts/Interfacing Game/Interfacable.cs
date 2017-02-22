using UnityEngine;
using System.Collections;

public class Interfacable : MonoBehaviour {

	private InterfaceInfo meta;
	public InterfaceInfo MetaInfo { get { return meta; } }



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision collision) {
		var other = collision.gameObject;
		if (other.GetComponent<InterfaceInteractor> () != null) {
		}
	}

	/// <summary>
	/// Transforms this object into a copy of the passed in object
	/// </summary>
	/// <param name="targetObj">Target object.</param>
	public void Interface(InterfaceInfo info) {
		meta = info;
		var targetMesh = info.TargetObject.GetComponent<MeshFilter> ().mesh;
		var targetCollider = info.TargetObject.GetComponent<MeshCollider> ();
		var targetRenderer = info.TargetObject.GetComponent<MeshRenderer> ();
		var meshFilter = GetComponent<MeshFilter> ();
		var meshRenderer = GetComponent<MeshRenderer> ();
		var meshCollider = GetComponent<MeshCollider> ();
		if (meshFilter != null) {
			meshFilter.mesh = targetMesh;
			transform.localScale = info.TargetObject.transform.localScale;
			meshRenderer.material = targetRenderer.material;
			meshCollider.sharedMesh = targetCollider.sharedMesh;
		}
	}
}

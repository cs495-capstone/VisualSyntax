using UnityEngine;
using System.Collections;

public class VroomObject : MonoBehaviour {

	// In reference mode, the objects within this object get pulled out and
	// lines are drawn to them.
	public bool ReferenceMode;

	// This is the transform object to connect to
	public Transform transform;

	// Top level objects have the label drawn. This is overridden by ReferenceMode -- in reference
	// mode all labels are drawn
	public bool TopLevelObject;

	public string Label;

	public delegate void RefModeEventHandler(object sender);
	public event RefModeEventHandler OnReferenceModeActivated;
	public event RefModeEventHandler OnReferenceModeDeactivated;

	private bool handledRefModeChange;

	private Vector3 size;

	private Transform subObject;

	private GameObject sampleTextMesh;

	// Use this for initialization
	void Start () {
		sampleTextMesh = GameObject.Find ("SampleTextMesh");
		subObject = transform.Find ("Object");
		size = subObject.GetComponent<Renderer> ().bounds.size;

		if (TopLevelObject) {
			CreateTextMesh ();
		}

		OnReferenceModeActivated += (object sender) => {
			int i = 1;
			foreach (var child in transform.GetComponentsInChildren<VroomObject>()) {
				child.transform.position += new Vector3(0, i * (size.y / 2), 0);
				child.CreateTextMesh();
				i++;
			}
		};


	}

	public void CreateTextMesh() {
		var textObj = GameObject.Instantiate (sampleTextMesh);
		textObj.name = Label + "_Label";

		var text = textObj.GetComponent<TextMesh> ();
		text.anchor = TextAnchor.MiddleCenter;
		text.text = Label;
		text.alignment = TextAlignment.Center;
		textObj.transform.parent = subObject;
		textObj.transform.position = subObject.position;
	}

	public void DestroyTextMesh() {
		GameObject.Destroy (GameObject.Find (Label + "_Label"));
	}
		

	private void HandleRefModeActivate(object sender) {
		// Move inner object outside this object
	}
	
	// Update is called once per frame
	void Update () {
		if (ReferenceMode) {
			if (!handledRefModeChange) {
				OnReferenceModeActivated (this);
				handledRefModeChange = true;
			}	
		} else if (!ReferenceMode) {
			if (handledRefModeChange) {
				OnReferenceModeDeactivated (this);
				handledRefModeChange = false;
			}
		}
	}
}

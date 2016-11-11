using UnityEngine;
using System.Collections;

public class VroomObject : MonoBehaviour {

	/// <summary>
	/// In reference mode, the objects within this object get pulled out and
	/// lines are drawn to them.
	/// </summary>

	public bool ReferenceMode;

	/// <summary>
	/// This is the transform object to connect to that controls the position.
	/// </summary> 
	public Transform transform;

	/// <summary>
	/// Top level objects have the label drawn. This is overridden by ReferenceMode -- in reference
	/// mode all labels are drawn
	/// </summary>
	public bool TopLevelObject;

	/// <summary>
	/// The label is what shows up when the object is viewed.
	/// </summary>
	public string Label;

	/// <summary>
	/// This flag is used to determine if the event handler has been called already. This is
	/// used so we don't continuously call the event handler when reference mode changes.
	/// </summary>
	private bool handledRefModeChange;

	/// <summary>
	/// This is the size of the object (x = width, y = height, z = depth)
	/// </summary>
	private Vector3 size;

	/// <summary>
	/// This is the transform used to control the position of the object within this vroom object
	/// </summary>
	private Transform subObject;

	/// <summary>
	/// This is the text mesh object used to put labels on objects.
	/// </summary>
	private GameObject sampleTextMesh;

	/// <summary>
	/// This is the line renderer used to draw lines between objects.
	/// </summary>
	private LineRenderer linker;

	/// <summary>
	/// This is the text mesh linked to this object
	/// </summary>
	private GameObject myTextMesh;

	public delegate void RefModeEventHandler(object sender);
	public event RefModeEventHandler OnReferenceModeActivated;
	public event RefModeEventHandler OnReferenceModeDeactivated;


	/// <summary>
	/// Initializes the vroom object and all it's fields and event handlers.
	/// </summary>
	void Start () {
		myTextMesh = null;

		linker = GameObject.Find ("ObjectLinker").GetComponent<LineRenderer> ();
		linker.SetWidth (0.1f, 0.1f);
		linker.SetColors (Color.green, Color.green);

		sampleTextMesh = GameObject.Find ("SampleTextMesh");

		subObject = transform.Find ("Object");
		size = subObject.GetComponent<Renderer> ().bounds.size;

		if (TopLevelObject) {
			CreateTextMesh ();
		}

		OnReferenceModeActivated += HandleRefModeActivate;
		OnReferenceModeDeactivated += HandleRefModeDeactivate;
	}

	/// <summary>
	/// Creates the text mesh to display the label.
	/// </summary>
	public void CreateTextMesh() {
		if (myTextMesh == null) {
			myTextMesh = GameObject.Instantiate (sampleTextMesh);
			myTextMesh.name = Label + "_Label";

			var text = myTextMesh.GetComponent<TextMesh> ();
			text.anchor = TextAnchor.MiddleCenter;
			text.text = Label;
			text.alignment = TextAlignment.Center;
			myTextMesh.transform.parent = subObject;
			myTextMesh.transform.position = subObject.position;
		}
	}

	/// <summary>
	/// Deletes the text mesh if this is not a top level object
	/// </summary>
	public void DeleteTextMesh() {
		if (myTextMesh != null) {
			GameObject.Destroy (myTextMesh);
			myTextMesh = null;
		}
	}
		
	/// <summary>
	/// Handles when reference mode is deactivated. It resets the positions of the children, 
	/// deletes the text meshes, and disables the line renderer.
	/// </summary>
	/// <param name="sender">Sender.</param>
	private void HandleRefModeDeactivate(object sender) {
		foreach (var child in transform.GetComponentsInChildren<VroomObject>()) {
			child.transform.position = transform.position;
			if (!child.TopLevelObject) child.DeleteTextMesh();
		}
		linker.SetVertexCount(0);
	}

	/// <summary>
	/// Handles when reference mode is activated. It pulls out the children objects and enables 
	/// their labels.
	/// </summary>
	/// <param name="sender">Sender.</param>
	private void HandleRefModeActivate(object sender) {
		int i = 1;
		ArrayList positions = new ArrayList();
		positions.Add(transform.position);
		foreach (var child in transform.GetComponentsInChildren<VroomObject>()) {
			if (!child.TopLevelObject) {
				child.transform.position += new Vector3(0, i * (size.y / 2), 0);
				positions.Add(child.transform.position);
				child.CreateTextMesh();
				i++;
			}
		}
		linker.SetVertexCount(positions.Count);
		Vector3[] vec = new Vector3[positions.Count];
		positions.CopyTo(vec);
		for (i = 0; i < vec.Length; i++) {
			linker.SetPosition(i, vec[i]);
		}
	}

	// Update is called once per frame
	void Update () {
		if (ReferenceMode) {
			if (!handledRefModeChange) {
				OnReferenceModeActivated (this);
				handledRefModeChange = true;
			}	
	
			UpdateLinkerPositions ();
		} else if (!ReferenceMode) {
			if (handledRefModeChange) {
				OnReferenceModeDeactivated (this);
				handledRefModeChange = false;
			}
		}
	}

	void UpdateLinkerPositions() {
		ArrayList positions = new ArrayList();
		positions.Add(transform.position);
		foreach (var child in transform.GetComponentsInChildren<VroomObject>()) {
			positions.Add(child.transform.position);
		}
		linker.SetVertexCount(positions.Count);
		Vector3[] vec = new Vector3[positions.Count];
		positions.CopyTo(vec);
		for (int i = 0; i < vec.Length; i++) {
			linker.SetPosition(i, vec[i]);
		}
	}
}

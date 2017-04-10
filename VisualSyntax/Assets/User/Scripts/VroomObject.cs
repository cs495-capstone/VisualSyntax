using UnityEngine;
using System.Collections;

public class VroomObject : MonoBehaviour {

	/// <summary>
	/// This is used for 180 degrees in calculations.
	/// </summary>
	private static float oneEighty = 180f;

	/// <summary>
	/// In reference mode, the objects within this object get pulled out and
	/// lines are drawn to them.
	/// </summary>

	public bool ReferenceMode;

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
	/// This is the text mesh linked to this object.
	/// </summary>
	private GameObject myTextMesh;

	/// <summary>
	/// This is how we are handling the reference mode interaction (along with the gun).
	/// </summary>
	public delegate void RefModeEventHandler(object sender);
	public event RefModeEventHandler OnReferenceModeActivated;
	public event RefModeEventHandler OnReferenceModeDeactivated;

	/// <summary>
	/// This is the list of line renderers that will be used to draw
	/// the references between objects and their fields.
	/// </summary>
	private ArrayList lineRenderers;

	/// <summary>
	/// This is a list of all object fields that will dislplay in reference mode
	/// </summary
	private ArrayList vroomChildren;

	/// <summary>
	/// Initializes the vroom object and all it's fields and event handlers.
	/// </summary>
	void Start () {
		//initialize to null/empty and pull objects hidden in the game world
		lineRenderers = new ArrayList ();
		vroomChildren = new ArrayList ();
		myTextMesh = null;

		linker = GameObject.Find ("ObjectLinker").GetComponent<LineRenderer> ();

		sampleTextMesh = GameObject.Find ("SampleTextMesh");

		subObject = transform.Find ("Object");
		size = subObject.GetComponentInChildren<MeshRenderer> ().bounds.size;

		foreach (var child in GetComponentsInChildren<VroomObject>()) {
			vroomChildren.Add (child);

			var tmp = GameObject.Instantiate (linker);
			tmp.SetVertexCount (2);
			lineRenderers.Add (tmp);
		}

		if (TopLevelObject) {
			CreateTextMesh ();
		}
		// We add our functionality on at the end rather than override
		// so we don't lose the functionality happening before us.
		OnReferenceModeActivated += HandleRefModeActivate;
		OnReferenceModeDeactivated += HandleRefModeDeactivate;
	}

	/// <summary>
	/// Creates the text mesh to display the label.
	/// </summary>
	public void CreateTextMesh() {
		if (myTextMesh == null) {
			myTextMesh = GameObject.Instantiate (sampleTextMesh);

			if (!TopLevelObject) {
				myTextMesh.transform.localScale *= 0.5f;
			}

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
		foreach (var childObj in vroomChildren) {
			var child = (VroomObject)childObj;
			if (!child.TopLevelObject) {
				if (child.GetComponentInChildren<MeshRenderer> () != null) {
					child.GetComponentInChildren<MeshRenderer> ().enabled = false;
				}
				child.DeleteTextMesh ();
				child.transform.position = transform.position;
			}
		}
		foreach (var linker in lineRenderers) {
			((LineRenderer)linker).SetVertexCount (0);
		}
	}

	public void SetLabel(string label) {
		//DeleteTextMesh ();
		this.Label = label;
		//CreateTextMesh ();
	}

	/// <summary>
	/// Handles when reference mode is activated. It pulls out the children objects and enables 
	/// their labels.
	/// </summary>
	/// <param name="sender">Sender.</param>
	private void HandleRefModeActivate(object sender) {
		var angle = oneEighty / (vroomChildren.Count);
		for (int j = 0; j < vroomChildren.Count; j++) {
			var child = (VroomObject) vroomChildren [j];
			var linker = (LineRenderer)lineRenderers [j];

			child.GetComponentInChildren<MeshRenderer> ().enabled = true;
			linker.SetVertexCount (2);
			if (!child.TopLevelObject) {
				var cpos = child.gameObject.transform.position;
				child.gameObject.transform.position = new Vector3 (cpos.x  + Mathf.Cos(j * Mathf.Deg2Rad * angle), cpos.y + 1.5f * Mathf.Sin(j * Mathf.Deg2Rad * angle), cpos.z);
				child.CreateTextMesh();
				linker.SetPosition (0, transform.position);
				linker.SetPosition (1, child.gameObject.transform.position);
			}
		}
	}

	/// <summary>
	/// Update is called once per frame
	/// </summary>
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

	/// <summary>
	/// This method is responsible for setting linkers between each object field and its parent.
	/// </summary>
	void UpdateLinkerPositions() {
		for (int i = 0; i < vroomChildren.Count; i++) {
			var child = (VroomObject) vroomChildren [i];
			var linker = (LineRenderer) lineRenderers [i];
			//parent side
			linker.SetPosition (0, transform.position);
			//child side
			linker.SetPosition (1, child.gameObject.transform.position);
		}
	}
}

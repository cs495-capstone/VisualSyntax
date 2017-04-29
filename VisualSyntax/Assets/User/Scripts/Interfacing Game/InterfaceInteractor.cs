using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class is holds the code used when interfaces interact with the game world.
/// </summary>
public class InterfaceInteractor : MonoBehaviour {

	/// <summary>
	/// This is the name of the interface that will be watched for.
	/// </summary>
	public string TargetName;

	/// <summary>
	/// This is the listeners to the interaction.
	/// </summary>
	private List<IEventListener> interactListeners = new List<IEventListener>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// This method is called when the target interface interacts with this object.
	/// </summary>
	/// <param name="collision">Collision of the interacting object.</param>
	void OnTriggerEnter(Collider collision) {
		var obj = collision.gameObject;
		var objInterface = obj.GetComponent<StaticType> ();
		if (objInterface != null) {
			var name = objInterface.Name;
			if (name == TargetName) {
				OnTargetEnter ();
			}
		}
	}

	/// <summary>
	/// This method broadcasts to each listener that an interaction has occured.
	/// </summary>
	void OnTargetEnter() {
		foreach (var listener in interactListeners) {
			listener.OnMessageReceived (this, new GameEventArgs () { Message = InterfacingGame.MSG_INTERACTED });
		}
	} 

	/// <summary>
	/// This is the method used to add listeners of this class.
	/// </summary>
	/// <param name="listener">Listener to add.</param>
	public void AddInteractListener(IEventListener listener) {
		interactListeners.Add (listener);
	}
}

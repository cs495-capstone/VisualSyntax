using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceInteractor : MonoBehaviour {

	public string TargetName;

	private List<IEventListener> interactListeners;

	// Use this for initialization
	void Start () {
		interactListeners = new List<IEventListener> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collision) {
		var obj = collision.gameObject;
		var objInterface = obj.GetComponent<Interfacable> ();
		if (objInterface != null) {
			var name = objInterface.MetaInfo.Name;
			if (name == TargetName) {
				OnTargetEnter ();
			}
		}
	}

	void OnTargetEnter() {
		foreach (var listener in interactListeners) {
			listener.OnMessageReceived (this, new GameEventArgs () { Message = InterfacingGame.MSG_INTERACTED });
		}
	} 

	public void AddInteractListener(IEventListener listener) {
		interactListeners.Add (listener);
	}
}

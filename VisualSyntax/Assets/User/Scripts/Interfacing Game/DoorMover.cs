using UnityEngine;
using System;
using System.Collections;

public class DoorMover : MonoBehaviour, IEventListener {

	readonly Vector3 LEFT_RIGHT_DEST = new Vector3 (2.757f, 0, 0);
	readonly Vector3 TOP_BOTTOM_DEST = new Vector3 (0, 1.799f, 0);

	bool moving = false;
	bool leftRight = true;
	Transform door1;
	Transform door2;

	// Use this for initialization
	void Start () {
		foreach (var interactable in GetComponentsInChildren<InterfaceInteractor>()) {
			interactable.AddInteractListener (this);
		}
		door1 = transform.GetChild (0);
		door2 = transform.GetChild (1);
		leftRight = door1.name == "Left";
	}
	
	// Update is called once per frame
	void Update () {
		if (moving) {
			Vector3 target1, target2;
			if (leftRight) {
				target1 = -LEFT_RIGHT_DEST;
				target2 = -target1;
			} else {
				target1 = TOP_BOTTOM_DEST;
				target2 = -target1;
			}
			door1.localPosition = Vector3.MoveTowards (door1.localPosition, target1, Time.deltaTime * 1.5f);
			door2.localPosition = Vector3.MoveTowards (door2.localPosition, target2, Time.deltaTime * 1.5f);

			moving = (door1.localPosition - target1).magnitude >= 0.05f;
		}
	}


	public void OnMessageReceived(object sender, EventArgs args) {
		var gameArgs = (GameEventArgs)args;
		if (gameArgs.Message == InterfacingGame.MSG_INTERACTED) {
			moving = true;
		}
	}
}

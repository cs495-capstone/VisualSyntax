using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// This class is responsible for moving the doors in the world.
/// </summary>
public class DoorMover : MonoBehaviour, IEventListener {

	/// <summary>
	/// This is the vecotr used to move the doors left and right.
	/// </summary>
	readonly Vector3 LEFT_RIGHT_DEST = new Vector3 (2.757f, 0, 0);

	/// <summary>
	/// This is the vector used to move doors up and down.
	/// </summary>
	readonly Vector3 TOP_BOTTOM_DEST = new Vector3 (0, 1.799f, 0);

	/// <summary>
	/// This flag holds whether the door is currently moving or not.
	/// </summary>
	bool moving = false;

	/// <summary>
	/// This flag is used to determine if a door is moving vertically
	/// or horizontally.
	/// </summary>
	bool leftRight = true;

	/// <summary>
	/// This is the tranform for one of the halves of a door.
	/// </summary>
	Transform door1;

	/// <summary>
	/// This is the transform for the other half of the door.
	/// </summary>
	Transform door2;

	/// <summary>
	/// The start method is used to initialize an object in Unity.
	/// It is used to populate the door objects as well as the 
	/// other flags.
	/// </summary>
	void Start () {
		foreach (var interactable in GetComponentsInChildren<InterfaceInteractor>()) {
			interactable.AddInteractListener (this);
		}
		door1 = transform.GetChild (0);
		door2 = transform.GetChild (1);
		leftRight = door1.name == "Left";
	}
	
	/// <summary>
	/// This method is used to update the positions of the doors based on the moving flag.
	/// This method is called once per frame.
	/// </summary>
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


	/// <summary>
	/// This method is used to alert a door that it should begin to move.
	/// </summary>
	/// <param name="sender">This is the object that is broadcasting to the door.</param>
	/// <param name="args">These are the arguments provided by the sender.</param>
	public void OnMessageReceived(object sender, EventArgs args) {
		var gameArgs = (GameEventArgs)args;
		if (gameArgs.Message == InterfacingGame.MSG_INTERACTED) {
			moving = true;
		}
	}
}

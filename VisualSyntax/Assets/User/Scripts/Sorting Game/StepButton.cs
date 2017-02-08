using System;
using System.Collections.Generic;
using VRTK;
using UnityEngine;

public class StepButton : MonoBehaviour
{
	public readonly string MSG_BUTTONPUSHED = "CHECK";
	///<summary>
	/// This is the list of listeners waiting for all blocks to be on the panels
	/// </summary>
	List<IEventListener> listeners = new List<IEventListener>();

	private void Start()
	{
		GetComponent<VRTK_Button>().events.OnPush.AddListener(handlePush);
	}

	private void handlePush()
	{
		Debug.Log("Step Button Pushed");
		Broadcast (MSG_BUTTONPUSHED);
	}

	/// <summary>
	/// Sends the event to each listener of this object.
	/// </summary>
	/// <param name="message">The message to send to every listener.</param>
	private void Broadcast(string message) {
		foreach (var listener in listeners) {
			listener.OnMessageReceived (this, new GameEventArgs (){ Message = message });
		}
	}

	/// <summary>
	/// Subscribe the specified subscriber.
	/// </summary>
	/// <param name="subscriber">Subscriber.</param>
	public void Subscribe(IEventListener subscriber) { 
		listeners.Add (subscriber);
	}
}

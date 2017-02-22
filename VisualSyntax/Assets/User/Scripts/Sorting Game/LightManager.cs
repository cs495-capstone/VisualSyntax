using System;
using System.Collections.Generic;
using UnityEngine;
public class LightManager : MonoBehaviour
{
	public readonly string MSG_DEATH = "LIGHTSOUT";

	private LifeLight[] lights;

	private int currentLight;

	private int numLights;

	///<summary>
	/// This is the list of listeners waiting for all lights to be off
	/// </summary>
	List<IEventListener> listeners = new List<IEventListener>();

	void Start () {
		//Right now we setup 5 panels
		lights = GetComponentsInChildren<LifeLight>();
		currentLight = 0;
		numLights = lights.Length;
	}

	public void RemoveLight() {
		if (currentLight < numLights) {
			lights [currentLight].LightStatus (false);
			currentLight++;
		} else {
			Broadcast (MSG_DEATH);
		}
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



using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages all the lights in a room.
/// </summary>
public class LightManager : MonoBehaviour
{
	/// <summary>
	/// This is the message used to signal death, or game off.
	/// </summary>
	public const string MSG_DEATH = "LIGHTSOUT";

	/// <summary>
	/// The lights in the room.
	/// </summary>
	private LifeLight[] lights;


	/// <summary>
	/// The light currently being used.
	/// </summary>
	private int currentLight;

	/// <summary>
	/// The number of lights in the room.
	/// </summary>
	private int numLights;

	///<summary>
	/// This is the list of listeners waiting for all lights to be off
	/// </summary>
	List<IEventListener> listeners = new List<IEventListener>();

	/// <summary>
	/// This method is used to initialize the list of lights.
	/// </summary>
	void Start () {
		//Right now we setup 5 panels
		lights = GetComponentsInChildren<LifeLight>();
		currentLight = 0;
		numLights = lights.Length;
	}

	/// <summary>
	/// This method is used to cut off a light.
	/// </summary>
	public void RemoveLight() {
		if (currentLight < numLights) {
			lights [currentLight].LightStatus (false);
			currentLight++;
			if (currentLight >= lights.Length) {
				Broadcast (MSG_DEATH);
			}
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



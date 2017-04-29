using VRTK;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The class representing the powerswitch that intializes the sort game.
/// </summary>
public class PowerSwitch : MonoBehaviour
{
	/// <summary>
	/// This is the message that will be sent on power on.
	/// </summary>
	public const string MSG_ON = "POWER SWITCH ON";

	/// <summary>
	/// This is used to keep track of whether the switch is able to be pulled.
	/// </summary>
	private bool _enabled;

	/// <summary>
	/// This is a list of objects that are listening for a status change in the 
	/// power switch.
	/// </summary>
	private List<IEventListener> listeners;

	/// <summary>
	/// This method is used to intialize the powerswitch.
	/// </summary>
	private void Start()
	{
		GetComponentInChildren<VRTK_Lever> ().gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
		listeners = new List<IEventListener> ();
		this.GetComponentInChildren<VRTK_Control>().defaultEvents.OnValueChanged.AddListener(HandleChange);
		HandleChange(this.GetComponentInChildren<VRTK_Control>().GetValue(), GetComponentInChildren<VRTK_Control>().GetNormalizedValue());
	}

	/// <summary>
	/// This method is called to handle when the switch is being moved.
	/// </summary>
	/// <param name="value">Value of movement.</param>
	/// <param name="normalizedValue">Normalized value of movement.</param>
	private void HandleChange(float value, float normalizedValue)
	{
		Debug.Log (value.ToString () + "(" + normalizedValue.ToString () + "%)");
		if (_enabled && value == -2) {
			foreach (var listener in listeners) {
				listener.OnMessageReceived (this, new GameEventArgs() { Message = MSG_ON });
			}
		}
	}
	/// <summary>
	/// This enables the powerswitch to be flipped.
	/// </summary>
	public void Enable() {
		if (_enabled == false) {
			GetComponentInChildren<VRTK_Lever> ().gameObject.GetComponent<Rigidbody> ().freezeRotation = false;
			var indicator = GameObject.Find ("PowerSwitchIndicator");
			indicator.GetComponent<MeshRenderer> ().material.color = new Color(0,1,0,0.75F);

		}
		_enabled = true;
	}

	/// <summary>
	/// This code is used to disable the powerswitch.
	/// </summary>
	public void Disable() {
		if (_enabled == true) {
			GetComponentInChildren<VRTK_Lever> ().gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
			var indicator = GameObject.Find ("PowerSwitchIndicator");
			indicator.GetComponent<MeshRenderer> ().material.color = new Color(1,1,1,0.25F);
		}
		_enabled = false;
	}

	/// <summary>
	/// This method returns the current value of enabled.
	/// </summary>
	/// <returns><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</returns>
	public bool IsEnabled() {
		return _enabled;
	}

	/// <summary>
	/// This method is used to subscribe as a listener to this object.
	/// </summary>
	/// <param name="listener">Listener.</param>
	public void Subscribe(IEventListener listener) {
		listeners.Add(listener);
	}
}
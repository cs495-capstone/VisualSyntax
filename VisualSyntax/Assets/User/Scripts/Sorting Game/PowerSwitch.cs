using VRTK;
using UnityEngine;
using System.Collections.Generic;

public class PowerSwitch : MonoBehaviour
{
	public const string MSG_ON = "POWER SWITCH ON";

	private bool _enabled;

	private List<IEventListener> listeners;

	private void Start()
	{
		GetComponentInChildren<VRTK_Lever> ().gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
		listeners = new List<IEventListener> ();
		this.GetComponentInChildren<VRTK_Control>().defaultEvents.OnValueChanged.AddListener(HandleChange);
		HandleChange(this.GetComponentInChildren<VRTK_Control>().GetValue(), GetComponentInChildren<VRTK_Control>().GetNormalizedValue());
	}

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
			indicator.GetComponent<MeshRenderer> ().material.color = Color.green;
		}
		_enabled = true;
	}

	public bool IsEnabled() {
		return _enabled;
	}

	public void Subscribe(IEventListener listener) {
		listeners.Add(listener);
	}
}
using VRTK;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
	public TextMesh go;
	private bool _enabled;

	private void Start()
	{
		GetComponent<VRTK_Control>().defaultEvents.OnValueChanged.AddListener(HandleChange);
		HandleChange(GetComponent<VRTK_Control>().GetValue(), GetComponent<VRTK_Control>().GetNormalizedValue());
	}

	private void HandleChange(float value, float normalizedValue)
	{
		go.text = value.ToString() + "(" + normalizedValue.ToString() + "%)";
	}

	/// <summary>
	/// This enables the powerswitch to be flipped.
	/// </summary>
	public void Enable() {
		if (_enabled == false) {
			var indicator = GameObject.Find ("PowerSwitchIndicator");
			indicator.GetComponent<MeshRenderer> ().material.color = Color.green;
		}
		_enabled = true;
	}

	public bool IsEnabled() {
		return _enabled;
	}
}
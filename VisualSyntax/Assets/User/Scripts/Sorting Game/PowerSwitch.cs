using VRTK;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
	public TextMesh go;
	private bool enabled;

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
		enabled = true;
	}
}
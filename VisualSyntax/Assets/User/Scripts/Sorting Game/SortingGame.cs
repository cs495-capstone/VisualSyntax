using System;
using System.Collections.Generic;
using UnityEngine;

public class SortingGame : MonoBehaviour, IEventListener
{
	private SortingPanels panels;
	private SortingManager sortingManager;
	private PowerSwitch powerSwitch;
	private StepButton stepButton;

	private State state;
	private List<IEventListener> gameListeners;

	void Start() {
		panels = GameObject.Find ("SortedPanels").GetComponent<SortingPanels>();
		powerSwitch = GameObject.Find ("PowerSwitch").GetComponent<PowerSwitch>();
		stepButton = GameObject.Find ("StepButton").GetComponent<StepButton>();
		panels.Subscribe (this);
	}

	void Update() {
		switch (state) {
		case State.DISABLED:
			break;
		case State.OFF:
			InitializePowerSwitch ();
			break;
		case State.ON:
			SetPanelValues ();
			InitializeSortManager ();
			state = State.WAIT;
			break;
		case State.WAIT:
			break;
		case State.CHECK:
			CheckList ();
			state = State.WAIT;
			break;
		case State.DONE:
			break;
		}
	}

	void InitializePowerSwitch() {
		//Initialize powerswitch to enabled
		Console.WriteLine("Enabling Power Switch...");
		powerSwitch.Enable ();
	}

	void SetPanelValues() {
		// Initialize panels to random integers
		Console.WriteLine("Setting panel values...");
		int[] list = { 1, 2, 3, 4, 5 };
		IsListValid (list);
	}

	bool IsListValid(int[] list) {
		// Checks if the passed in list is valid (i.e. sorted)
		Console.WriteLine("Chekcing Valid List");
	}

	void InitializeSortManager() {
		// Initializes the sorting manager
		Console.WriteLine("Initializing sort manager...");
	}

	void CheckList() {
		// Calls the sorting manager's check function
		Console.WriteLine("Checking List...");
	}

	void TriggerGameComplete() {
		// Sends an event to listeners waiting for this game to finish
		Console.WriteLine("Game Completed...");
		foreach (IEventListener listener in gameListeners) {
			listener.OnMessageReceived (this, EventArgs.Empty);
		}
	}
		
	void OnPowerSwitchOn() {
		// [EVENT] Called when the power switch is flipped
		Console.WriteLine("Power switch on, changing state to ON...");
		state = State.ON;
	}

	void OnStepButtonPress() {
		// [EVENT] Called when step button is pushed
		Console.WriteLine("Button pressed, changing state to CHECK...");
		state = State.CHECK;
	}

	void OnBlocksLockedIn() {
		// [EVENT] Called when all five blocks are locked on panels
		Console.WriteLine ("All Blocks Locked In...");
		state = State.OFF;
	}

	void OnMessageReceived(object sender, EventArgs args) {
		if (sender.GetType is SortingPanels) {
			state = State.OFF;
		} 
	}

	enum State {
		DISABLED, OFF, ON, WAIT, CHECK, DONE
	}
}

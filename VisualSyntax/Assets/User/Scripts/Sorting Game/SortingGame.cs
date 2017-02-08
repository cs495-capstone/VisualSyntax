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
		gameListeners = new List<IEventListener> ();
		panels = GameObject.Find ("SortedPanels").GetComponent<SortingPanels>();
		powerSwitch = GameObject.Find ("PowerSwitch").GetComponent<PowerSwitch>();
		stepButton = GameObject.Find ("StepButton").GetComponent<StepButton>();
		panels.Subscribe (this);
		stepButton.Subscribe (this);
	}

	void Update() {
		switch (state) {
		case State.DISABLED:
			break;
		case State.OFF:
			InitializePowerSwitch ();
			if (Input.GetKeyDown(KeyCode.P)) {
				state = State.ON;
				Debug.Log ("State = " + state);
			}
			break;
		case State.ON:
			SetPanelValues ();
			InitializeSortManager ();
			state = State.WAIT;
			Debug.Log ("State = " + state);
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
		if (!powerSwitch.IsEnabled()) {
			Debug.Log("Enabling Power Switch...");
			powerSwitch.Enable ();
		}
	}

	void SetPanelValues() {
		// Initialize panels to random integers
		Debug.Log("Setting panel values...");
		int[] list = GetShuffledList();
		panels.InitializePanels (list);
	}

	int[] GetShuffledList() {
		int[] result = new int[panels.Count];
		for (int i = 0; i < result.Length; i++) {
			result [i] = UnityEngine.Random.Range(0, 100);
		}
		Shuffle (result);
		return result;
	}

	private void Shuffle(int[] array) {
		for (int i = 0; i < array.Length; i++) {
			int tmp = array [i];
			int r = UnityEngine.Random.Range (i, array.Length);
			array [i] = r;
			array [r] = tmp;
		}
	}

	private bool IsDistinct(int[] array) {
		bool distinct = true;
		int last = array [0];
		for (int i = 1; i < array.Length && distinct; i++) {
			distinct = array [i] == last;
			last = array [i];
		}
		return distinct;
	}

	bool IsListValid(int[] list) {
		// Checks if the passed in list is valid (i.e. sorted)
		Debug.Log("Chekcing Valid List");
		return false;
	}

	void InitializeSortManager() {
		// Initializes the sorting manager
		Debug.Log("Initializing sort manager...");
		sortingManager = new SortingManager (panels.GetValues(), new SelectionSort ());
	}

	void CheckList() {
		// Calls the sorting manager's check function
		Debug.Log("Checking List...");

		sortingManager.SetCurrentList (panels.GetValues ());
		sortingManager.Update ();
	}

	void TriggerGameComplete() {
		// Sends an event to listeners waiting for this game to finish
		Debug.Log("Game Completed...");
		foreach (IEventListener listener in gameListeners) {
			listener.OnMessageReceived (this, EventArgs.Empty);
		}
	}
		
	void OnPowerSwitchOn() {
		// [EVENT] Called when the power switch is flipped
		Debug.Log("Power switch on, changing state to ON...");
		state = State.ON;
		Debug.Log ("State = " + state);
	}

	void OnStepButtonPress() {
		// [EVENT] Called when step button is pushed
		Debug.Log("Button pressed, changing state to CHECK...");
		state = State.CHECK;
		Debug.Log ("State = " + state);
	}

	void OnBlocksLockedIn() {
		// [EVENT] Called when all five blocks are locked on panels
		if (state == State.DISABLED) {
			Debug.Log ("All Blocks Locked In...");
			state = State.OFF;
			Debug.Log ("State = " + state);
		}
	}

	void OnBlocksUnLocked() {
		if (state == State.OFF) {
			Debug.Log ("Blocks Removed Before Power ON");
			state = State.DISABLED;
			Debug.Log ("State = " + state);
		}
	}

	public void OnMessageReceived(object sender, EventArgs args) {
		GameEventArgs gameargs = (GameEventArgs)args;
		if (gameargs.Message == panels.MSG_INITIALIZED) {
			OnBlocksLockedIn ();
		} else if (gameargs.Message == panels.MSG_UNITIALIZED) { 
			OnBlocksUnLocked ();
		} else if (gameargs.Message == stepButton.MSG_BUTTONPUSHED) {
			OnStepButtonPress ();
		}
	}

	enum State {
		DISABLED, OFF, ON, WAIT, CHECK, DONE
	}
}

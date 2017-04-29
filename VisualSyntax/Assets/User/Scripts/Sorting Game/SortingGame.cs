using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the class that runs the entire sorting game.
/// </summary>
public class SortingGame : MonoBehaviour, IEventListener
{
	/// <summary>
	/// This is the object that holds all the game panels used to sort.
	/// </summary>
	private SortingPanels panels;
	/// <summary>
	/// This is the object that will manage the completion of the sorting
	/// algorthim.
	/// </summary>
	private SortingManager sortingManager;
	/// <summary>
	/// This is the switch used to power on the sorting game.
	/// </summary>
	private PowerSwitch powerSwitch;
	/// <summary>
	/// This is the object used to signal the user has ended their turn.
	/// </summary>
	private StepButton stepButton;
	/// <summary>
	/// This is the object that manages the lights in th room.
	/// </summary>
	private LightManager lightManager;
	/// <summary>
	/// The following are sources of ambient sound used in the game.
	/// </summary>
	private AudioSource generatorNoise;
	private AudioSource onNoise;
	private AudioSource offNoise;
	private AudioSource enableNoise;
	private AudioSource stepNoise;

	/// <summary>
	/// This is the state that the game is currently in.
	/// </summary>
	private State state;

	/// <summary>
	/// This is the list of objects listening to the progress of the game.
	/// </summary>
	private List<IEventListener> gameListeners;

	/// <summary>
	/// This is the method used when unity is initialized, and it is used to initialize the objects
	/// in the sorting game.
	/// </summary>
	void Start() {
		gameListeners = new List<IEventListener> ();
		panels = GameObject.Find ("SortedPanels").GetComponent<SortingPanels>();
		powerSwitch = GameObject.Find ("PowerSwitch").GetComponent<PowerSwitch>();
		stepButton = GameObject.Find ("StepButton").GetComponent<StepButton>();
		lightManager = GameObject.Find ("House").GetComponentInChildren<LightManager> ();
		generatorNoise = GameObject.Find ("GeneratorSound").GetComponent<AudioSource> ();
		onNoise = GameObject.Find ("OnSound").GetComponent<AudioSource> ();
		offNoise = GameObject.Find ("OffSound").GetComponent<AudioSource> ();
		enableNoise = GameObject.Find ("EnableSound").GetComponent<AudioSource> ();
		stepNoise = GameObject.Find ("StepButtonSound").GetComponent<AudioSource> ();
		panels.Subscribe (this);
		stepButton.Subscribe (this);
		powerSwitch.Subscribe (this);
		lightManager.Subscribe (this);
	}

	/// <summary>
	/// This method is run every frame, and keeps track of what state the game is currently in.
	/// </summary>
	void Update() {
		switch (state) {
		case State.DISABLED:
			UnInitializePowerSwitch ();
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
			break;
		case State.DONE:

			var wall = GameObject.Find ("Revealing Wall");
			var wallPos = wall.transform.position;
			var targetPos = new Vector3 (wallPos.x, wallPos.y, -8.02f);
			wall.transform.position = Vector3.MoveTowards (wallPos, targetPos, 0.5f * Time.deltaTime);

			//Debug.Log ("WE ARE DONEEEEE");
			break;
		case State.DEAD:
			break;
		}
	}

	/// <summary>
	/// This is the method used to initialize the power switch
	/// and progress in the game.
	/// </summary>
	void InitializePowerSwitch() {
		//Initialize powerswitch to enabled
		if (!powerSwitch.IsEnabled()) {
			Debug.Log("Enabling Power Switch...");
			powerSwitch.Enable ();
			enableNoise.Play ();
		}
	}

	/// <summary>
	/// This method is used to power off the switch if 
	/// the user disconnects the objects.
	/// </summary>
	void UnInitializePowerSwitch() {
		//Initialize powerswitch to enabled
		if (powerSwitch.IsEnabled()) {
			Debug.Log("Disabling Power Switch...");
			powerSwitch.Disable ();
			enableNoise.Play ();
		}
	}

	/// <summary>
	/// This method is used to set the values of the objects on the sorting panels
	/// </summary>
	void SetPanelValues() {
		// Initialize panels to random integers
		Debug.Log("Setting panel values...");
		int[] list = GetShuffledList();
		panels.InitializePanels (list);
	}

	/// <summary>
	/// This method shuffles the list and returns the integer array representing it.
	/// </summary>
	/// <returns>The shuffled list.</returns>
	int[] GetShuffledList() {
		int[] result = new int[panels.Count];
		for (int i = result.Length - 1; i > 0; i--) {
			result [i] = UnityEngine.Random.Range(0, 100);
		}
		Shuffle (result);
		return result;
	}

	/// <summary>
	/// This mehtod is used to shuffle the provided array.
	/// </summary>
	/// <param name="array">Array to shuffle.</param>
	private void Shuffle(int[] array) {
		for (int i = 0; i < array.Length; i++) {
			int tmp = array [i];
			int r = UnityEngine.Random.Range (i, array.Length);
			array [i] = r;
			array [r] = tmp;
		}
	}

	/// <summary>
	/// Determines whether the specified array is distinct.
	/// </summary>
	/// <returns><c>true</c> if this instance is distinct the specified array; otherwise, <c>false</c>.</returns>
	/// <param name="array">Array.</param>
	private bool IsDistinct(int[] array) {
		bool distinct = true;
		int last = array [0];
		for (int i = 1; i < array.Length && distinct; i++) {
			distinct = array [i] == last;
			last = array [i];
		}
		return distinct;
	}

	/// <summary>
	/// This method is used to determine the validity of the list
	/// </summary>
	/// <returns><c>true</c> if this instance is list valid the specified list; otherwise, <c>false</c>.</returns>
	/// <param name="list">List.</param>
	bool IsListValid(int[] list) {
		// Checks if the passed in list is valid (i.e. sorted)
		Debug.Log("Chekcing Valid List");
		return false;
	}

	/// <summary>
	/// This method is used to initalize the SortManager for use in the game.
	/// </summary>
	void InitializeSortManager() {
		// Initializes the sorting manager
		Debug.Log("Initializing sort manager...");
		sortingManager = new SortingManager (panels.GetValues(), new SelectionSort ());
	}

	/// <summary>
	/// This method is used to check the correctness of the list.
	/// </summary>
	void CheckList() {
		// Calls the sorting manager's check function
		Debug.Log("Checking List...");

		// -1.215
		// -8.02

		sortingManager.SetCurrentList (panels.GetValues ());
		var progressed = sortingManager.Update ();
		if (!progressed) { 
			lightManager.RemoveLight();
		}

		if (sortingManager.IsDone ()) {
			state = State.DONE;
		} else {
			state = State.WAIT;
		}
	}

	/// <summary>
	/// This method is called when the game is completed.
	/// </summary>
	void TriggerGameComplete() {
		// Sends an event to listeners waiting for this game to finish
		Debug.Log("Game Completed...");
		foreach (IEventListener listener in gameListeners) {
			listener.OnMessageReceived (this, EventArgs.Empty);
		}
	}
		
	/// <summary>
	/// This method is used when the powerswitch is pulled down.
	/// </summary>
	void OnPowerSwitchOn() {
		// [EVENT] Called when the power switch is flipped
		Debug.Log("Power switch on, changing state to ON...");
		onNoise.Play ();
		generatorNoise.mute = false;
		state = State.ON;
		Debug.Log ("State = " + state);
	}

	/// <summary>
	/// This method is called when the step button is selected.
	/// </summary>
	void OnStepButtonPress() {
		// [EVENT] Called when step button is pushed
		if (state == State.WAIT) {
			Debug.Log ("Button pressed, changing state to CHECK...");
			state = State.CHECK;
			Debug.Log ("State = " + state);
			stepNoise.Play ();
		}
	}

	/// <summary>
	/// This method is called when all objects are clicked in.
	/// </summary>
	void OnBlocksLockedIn() {
		// [EVENT] Called when all five blocks are locked on panels
		if (state == State.DISABLED) {
			Debug.Log ("All Blocks Locked In...");
			state = State.OFF;
			Debug.Log ("State = " + state);
		}
	}

	/// <summary>
	/// This method is called when a block is removed.
	/// </summary>
	void OnBlocksUnLocked() {
		if (state == State.OFF) {
			Debug.Log ("Blocks Removed Before Power ON");
			state = State.DISABLED;
			Debug.Log ("State = " + state);
		}
	}

	/// <summary>
	/// This method recieves messages from other classes and interprets what action
	/// is required.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="args">Arguments.</param>
	public void OnMessageReceived(object sender, EventArgs args) {
		GameEventArgs gameargs = (GameEventArgs)args;
		if (gameargs.Message == panels.MSG_INITIALIZED) {
			OnBlocksLockedIn ();
		} else if (gameargs.Message == panels.MSG_UNITIALIZED) { 
			OnBlocksUnLocked ();
		} else if (gameargs.Message == stepButton.MSG_BUTTONPUSHED) {
			OnStepButtonPress ();
		} else if (gameargs.Message == PowerSwitch.MSG_ON) {
			OnPowerSwitchOn ();
		} else if (gameargs.Message == LightManager.MSG_DEATH) {
			OnDeath ();
		}
	}

	/// <summary>
	/// This method is called when the user fails the game.
	/// </summary>
	void OnDeath() {
		state = State.DEAD;
		offNoise.Play ();
		generatorNoise.mute = true;
	}

	/// <summary>
	/// These are the states the game can be in.
	/// </summary>
	enum State {
		DISABLED, OFF, ON, WAIT, CHECK, DONE, DEAD
	}
}

using UnityEngine;
using System.Collections;
/// <summary>
/// This class is responsible for managing the progress of a sorting algorithm.
/// </summary>
public class SortingManager {
	/// <summary>
	/// This is the list at it's initial state before user begins acting on it.
	/// </summary>
	private int[] originalList;
	/// <summary>
	/// This is the list at it's last successful iteration of the sorting method chosen.
	/// </summary>
	private int[] currentList;
	/// <summary>
	/// This is the state of the list after the next step of the chosen sort.
	/// </summary>
	private int[] nextList;
	/// <summary>
	/// This is the algorithm the SortManager will call to figure out the next step.
	/// </summary>
    private IAlgorithm currentAlgorithm;
	/// <summary>
	/// This is the state that the users list in the Unity world is in.
	/// </summary>
	private int[] unityArrayList;

	/// <summary>
	/// Initializes a new instance of the <see cref="SortManager"/> class.
	/// </summary>
	/// <param name="unityList">Unity list to monitor.</param>
	/// <param name="currentAlgorithm">Current algorithm used to sort.</param>
	public SortingManager(int[] unityList, IAlgorithm currentAlgorithm) {
        this.unityArrayList = unityList;
        this.originalList = unityList;
        this.currentAlgorithm = currentAlgorithm;
        this.currentList = this.originalList;
		this.nextList = this.currentAlgorithm.nextStep(this.currentList);
    }

	/// <summary>
	/// This method is called once per frame
	/// </summary>
    public bool Update(){
        //This is where we check for correct placement.
        var nextMatch = this.checkMatch(this.nextList, this.unityArrayList);
        var currentMatch = this.checkMatch(this.currentList, this.unityArrayList);
		var progress = true;
        //If correct set currentList to nextList.  Set nextList to currentAlgorithm.nextStep();
        if(nextMatch){
			var temporary = this.currentList;
			this.currentList = this.nextList;
			this.nextList = this.currentAlgorithm.nextStep(temporary);
        }
        //Else if the order is not the currentList reset
        else if(!currentMatch) {
			progress = false;
        }
		return progress;
    }

	/// <summary>
	/// This method checks if ArrayListOne matches ArrayListTwo
	/// </summary>
	/// <returns><c>true</c>, if ArrayListOne == ArrayListTwo, <c>false</c> otherwise.</returns>
	/// <param name="ArrayListOne">The first list to compare</param>
	/// <param name="ArrayListTwo">The second list to compare</param>
	public bool checkMatch(int[] ArrayListOne, int[] ArrayListTwo) {
        //Check if the order of the list matches the nextList
        var match = true;
		if (ArrayListOne.Length == ArrayListTwo.Length) {
			for (int i = 0; i < ArrayListOne.Length && match; i = i + 1) {
				match = ArrayListOne [i] == ArrayListTwo [i];
			}
		} else {
			match = false;
		}
        return match;
    }

	/// <summary>
	/// This will change the unity list back to its last successful state
	/// </summary>
    public void revert() {
        this.unityArrayList = this.currentList;
    }

	/// <summary>
	/// This method will reset the unity list back to its original state
	/// </summary>
    public void reset() {
        this.unityArrayList = this.nextList;
    }

	/// <summary>
	/// This method sets the array equal to the one provided.
	/// </summary>
	/// <param name="array">Array.</param>
	public void SetCurrentList(int[] array) {
		unityArrayList = array;
	}

	/// <summary>
	/// This method checks to see if an algorthim is complete
	/// </summary>
	/// <returns><c>true</c> if this algorithm is done is done <c>true</c>; otherwise, <c>false</c>.</returns>
	public bool IsDone() {
		return currentAlgorithm.IsDone ();
	}
}
using UnityEngine;
using System.Collections;

public class SortManager {
	private object[] originalList;
	private object[] currentList;
	private object[] nextList;
    private IAlgorithm currentAlgorithm;
	private object[] unityArrayList;

	public SortManager(object[] unityList, IAlgorithm currentAlgorithm) {
        this.unityArrayList = unityList;
        this.originalList = unityList;
        this.currentAlgorithm = currentAlgorithm;
        this.currentList = this.originalList;
		this.nextList = this.currentAlgorithm.nextStep(this.currentList);
    }

    public void Update(){
        //This is where we check for correct placement.
        var nextMatch = this.checkStep(this.nextList, this.unityArrayList);
        var currentMatch = this.checkStep(this.currentList, this.unityArrayList);

        //If correct set currentList to nextList.  Set nextList to currentAlgorithm.nextStep();
        if(nextMatch){
			var temporary = this.currentList;
			this.currentList = this.nextList;
			this.nextList = this.currentAlgorithm.nextStep(temporary);
        }
        //Else if the order is not the currentList reset
        else if(!currentMatch) {
			this.unityArrayList = this.currentList;
        }
    }

	public bool checkStep(object[] ArrayListOne, object[] ArrayListTwo) {
        //Check if the order of the list matches the nextList
        var match = true;
        if(ArrayListOne.Length == ArrayListTwo.Length) {
            for (int i = 0; i < ArrayListOne.Length && match; i = i + 1) {
				match = ((SortingPanel)ArrayListOne[i]).GetValue() == ((SortingPanel)ArrayListTwo[i]).GetValue();
            }
        }
        return match;
    }

    public void revert() {
        this.unityArrayList = this.currentList;
    }

    public void reset() {
        this.unityArrayList = this.nextList;
    }
}
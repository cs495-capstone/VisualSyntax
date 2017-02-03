using UnityEngine;
using System.Collections;

/// <summary>
/// This is our first sorting algorithm implemented.
/// It performs one step of the selection sort algorithm.
/// </summary>
public class SelectionSort : IAlgorithm {
	/// <summary>
	/// This is the current index that the sort will begin with.
	/// </summary>
    private int currentIndex;

	/// <summary>
	/// Initializes a new instance of the <see cref="SelectionSort"/> class.
	/// </summary>
    public SelectionSort() {
        this.currentIndex = 0;
    }

	/// <summary>
	/// This method calculates the state of the list after the next step of
	/// the selection sort, and returns the list at the next step.
	/// </summary>
	/// <returns>The list after the next step.</returns>
	/// <param name="currentList">Current list.</param>
	public object[] nextStep(object[] currentList) {
		//Get a subset of the list that will be used to sort
		var activeList = (new ArrayList(currentList)).GetRange (this.currentIndex, currentList.Length - this.currentIndex);
		//smallest index is the smallest in our active list
        int smallestIndex = 0;
        for (int i = 0; i < activeList.Count; i = i + 1) {
			var a = (int)(((SortingPanel)activeList [i]).GetValue());
			var b = (int)(((SortingPanel)activeList [smallestIndex]).GetValue());

			if (a < b) {
                smallestIndex = i;
            }
        }
		//swap the currentIndex and the new smallest.
        swap(currentList,this.currentIndex, smallestIndex);
		return currentList;
    }

	/// <summary>
	/// This method swaps indexOne and indexTwo in the list arrayList
	/// </summary>
	/// <param name="array">array to swap on.</param>
	/// <param name="indexOne">Index one to swap.</param>
	/// <param name="indexTwo">Index two to swap.</param>
	public void swap(object[] array, int indexOne, int indexTwo) {
        object temporary = array[indexOne];
        array[indexOne] = array[indexTwo];
		array[indexTwo] = (UnityEngine.Object)temporary;
    }
}
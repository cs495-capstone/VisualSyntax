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
	public int[] nextStep(int[] currentList) {
		int smallestIndex = currentIndex;
		for (int i = currentIndex + 1; i < currentList.Length; i = i + 1) {
			var a = (int)(currentList[i]);
			var b = (int)(currentList[smallestIndex]);

			if (a < b) {
                smallestIndex = i;
            }
        }
		//swap the currentIndex and the new smallest.
		swap(currentList,smallestIndex, currentIndex);
		this.currentIndex++;
		return currentList;
    }

	/// <summary>
	/// This method swaps indexOne and indexTwo in the list arrayList
	/// </summary>
	/// <param name="array">array to swap on.</param>
	/// <param name="indexOne">Index one to swap.</param>
	/// <param name="indexTwo">Index two to swap.</param>
	public void swap(int[] array, int indexOne, int indexTwo) {
		int temporary = array[indexOne];
        array[indexOne] = array[indexTwo];
		array[indexTwo] = temporary;
    }
}
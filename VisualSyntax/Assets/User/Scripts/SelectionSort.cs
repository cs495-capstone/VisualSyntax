using UnityEngine;
using System.Collections;

public class SelectionSort : IAlgorithm {
    private int currentIndex;
	
    public SelectionSort() {
        this.currentIndex = 0;
    }

	public object[] nextStep(object[] currentList) {
		var activeList = (new ArrayList(currentList)).GetRange (this.currentIndex, currentList.Length - this.currentIndex);
        int smallestIndex = 0;
        for (int i = 0; i < activeList.Count; i = i + 1) {
			var a = (int)(((SortingPanel)activeList [i]).GetValue());
			var b = (int)(((SortingPanel)activeList [smallestIndex]).GetValue());

			if (a < b) {
                smallestIndex = i;
            }
        }
        swap(currentList,this.currentIndex, smallestIndex);
		return currentList;
    }

	public void swap(object[] arrayList, int indexOne, int indexTwo) {
        object temporary = arrayList[indexOne];
        arrayList[indexOne] = arrayList[indexTwo];
		arrayList[indexTwo] = (UnityEngine.Object)temporary;
    }
}
using UnityEngine;
using System.Collections;

/// <summary>
/// This is the Algorithm interface which
/// makes sure each algorithm has a function
/// for calculating a sort step by step.
/// </summary>
public interface IAlgorithm {
	/// <summary>
	/// This method should calcualte the state
	/// of a list after one step of the sorting
	/// algorithm.
	/// </summary>
	/// <returns>The list at the next step.</returns>
	/// <param name="currentList">Current list to
	/// run the sort on.</param>
	object[] nextStep(object[] currentList);
}
using UnityEngine;
using System.Collections;

public interface IAlgorithm {
	object[] nextStep(object[] currentList);
}
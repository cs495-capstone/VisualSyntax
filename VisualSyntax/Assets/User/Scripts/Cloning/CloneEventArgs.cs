using System;
using UnityEngine;

/// <summary>
/// This is the class responsible for holding the new dynamic type 
/// when a shallow clone or parent changes.
/// </summary>
public class CloneEventArgs  : EventArgs
{
	/// <summary>
	/// This is a GameObject that holds the value of the new game
	/// object attached to the clone.
	/// </summary>
	/// <value>The new key GameObject.</value>
	public GameObject NewKey { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="CloneEventArgs"/> class.
	/// </summary>
	public CloneEventArgs ()
	{
	}
}


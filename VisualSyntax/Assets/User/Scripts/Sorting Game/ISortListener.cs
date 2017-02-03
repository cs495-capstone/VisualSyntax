using UnityEngine;
using System;

/// <summary>
/// This is the interface for classes that will be listening for sorting on 
/// our panels
/// </summary>
public interface ISortListener
{
	/// <summary>
	/// This is the method determining what happens when an item snaps onto the 
	/// sorting panel
	/// </summary>
	void OnSnap();
	/// <summary>
	/// This is the mehtod determining what happens when an item is unsnapped from
	///  the sorting panel
	/// </summary>
	void OnUnsnap();
}

using System;
using UnityEngine;

public class CloneEventArgs  : EventArgs
{
	public GameObject NewKey { get; set; }

	public CloneEventArgs ()
	{
	}
}


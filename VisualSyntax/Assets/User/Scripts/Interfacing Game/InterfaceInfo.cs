using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Interface info is the class that holds data about an interface
/// </summary>
public class InterfaceInfo
{
	/// <summary>
	/// This is the name of the interface.
	/// </summary>
	/// <value>The string holding interface name.</value>
	public string Name {get;set;}

	/// <summary>
	/// This is the target object that is being observed.
	/// </summary>
	/// <value>The object whose interactions are monitored.</value>
	public GameObject TargetObject { get; set; }

	/// <summary>
	/// This is the dictionary that holds all the settings for the interface.
	/// </summary>
	/// <value>The settings of the interface.</value>
	public Dictionary<string, object> Settings {get;set;}

	/// <summary>
	/// This method is used to add a key value pair to the dictionary.
	/// </summary>
	/// <param name="key">Key to add.</param>
	/// <param name="data">Data to add.</param>
	public void AddSetting(string key, object data) {
		Settings.Add (key, data);
	}


	/// <summary>
	/// This method is used to retrieve settings.
	/// </summary>
	/// <returns>The setting that matches the key provided.</returns>
	/// <param name="key">Key to return data on.</param>
	public object GetSetting(string key) {
		return Settings [key];
	}
}


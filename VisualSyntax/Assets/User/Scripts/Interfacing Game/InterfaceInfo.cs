using System;
using UnityEngine;
using System.Collections.Generic;

public class InterfaceInfo
{
	public string Name {get;set;}
	public GameObject TargetObject { get; set; }
	public Dictionary<string, object> Settings {get;set;}

	public void AddSetting(string key, object data) {
		Settings.Add (key, data);
	}

	public object GetSetting(string key) {
		return Settings [key];
	}
}


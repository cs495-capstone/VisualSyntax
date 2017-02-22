using UnityEngine;
using System.Collections;

public class ShallowClone : MonoBehaviour, IEventListener
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnMessageReceived(object newKey, System.EventArgs ea) {
		//Get Rid of Children
		this.transform.DetachChildren ();
		//Add new child
		var gameObject = GameObject.Instantiate ((GameObject) newKey);
		gameObject.transform.parent = this.transform;
	}
}


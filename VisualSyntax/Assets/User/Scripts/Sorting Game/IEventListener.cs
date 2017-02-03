using System;

public interface IEventListener
{
	void OnMessageReceived(object sender, EventArgs args);
}
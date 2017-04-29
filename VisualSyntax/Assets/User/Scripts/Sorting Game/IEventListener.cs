using System;

/// <summary>
/// This interface allows a class to be a listener to another.
/// </summary>
public interface IEventListener
{
	/// <summary>
	/// This function will determine the classes actions after it is broadcasted to.
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="args">Arguments.</param>
	void OnMessageReceived(object sender, EventArgs args);
}
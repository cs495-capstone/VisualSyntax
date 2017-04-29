using System;
	/// <summary>
	/// This class is used to pass messages between objects in the sorting game.
	/// </summary>
	public class GameEventArgs  : EventArgs
	{
		/// <summary>
		/// The message passed by the sender.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }

		public GameEventArgs ()
		{
		}
	}


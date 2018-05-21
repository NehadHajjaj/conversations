using System;

namespace Conversations.Core.Exceptions
{
    /// <summary>
	/// Generic exception raised by the UNOPS.Conversations.
	/// </summary>
	public class ConversationException : Exception
	{
		public ConversationException(string message)
			: base(message)
		{
		}
	}
}
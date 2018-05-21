namespace Conversations.Core.Domain
{
	public class ConversationIdentifier : IIdentifier<int>
	{
		public ConversationIdentifier(int value)
		{
			this.Value = value;
		}

		public int Value { get; private set; }
	}
}
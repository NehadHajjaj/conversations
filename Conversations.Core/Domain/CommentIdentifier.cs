namespace Conversations.Core.Domain
{
	public class CommentIdentifier : IIdentifier<int>
	{
		public CommentIdentifier(int value)
		{
			this.Value = value;
		}

		public int Value { get; private set; }
	}
}
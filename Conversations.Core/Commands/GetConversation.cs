namespace Conversations.Core.Commands
{
	using Conversations.Core.Commands.Core;
	using Conversations.Core.Domain;
	using Conversations.Core.Repositories;

	/// <summary>
	///     Get conversation .
	/// </summary>
	public class GetConversation : RequestHandler<GetConversation.Request, ConversationData>
	{
		private readonly IConversationsRepository context;

		public GetConversation(IConversationsRepository context)
		{
			this.context = context;
		}

		public override ConversationData Handle(Request command)
		{
			return this.context.GetConversation(command.Id);
		}

		public class Request
		{
			public readonly int Id;

			public Request(int id)
			{
				this.Id = id;
			}
		}
	}
}
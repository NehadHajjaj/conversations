namespace Conversations.Core.Commands
{
	using System.Collections.Generic;
	using Conversations.Core.Commands.Core;
	using Conversations.Core.Domain;
	using Conversations.Core.Repositories;

	/// <summary>
	///     Get list of opned conversation .
	/// </summary>
	public class GetConversations : RequestHandler<GetConversations.Request, IEnumerable<ConversationData>>
	{
		private readonly IConversationsRepository context;

		public GetConversations(IConversationsRepository context)
		{
			this.context = context;
		}

		public override IEnumerable<ConversationData> Handle(Request command)
		{
			return command.Opened ? this.context.GetOpenedConversations() : this.context.GetAllConversations();
		}

		public class Request
		{
			public bool Opened { get; set; }
		}
	}
}
namespace Conversations.Core.Commands
{
	using Conversations.Core.Commands.Core;
	using Conversations.Core.Repositories;

	/// <summary>
	///     Delete a document from an existing conversation.
	/// </summary>
	public class DeleteDocument : RequestHandler<DeleteDocument.Request>
	{
		private readonly IConversationsRepository context;

		public DeleteDocument(IConversationsRepository context)
		{
			this.context = context;
		}

		public override void Handle(Request command)
		{
			this.context.RemoveConversationDocument(command.Id);
		}

		public class Request
		{
			public readonly int Id;
			public readonly int UserId;

			public Request(int id, int userId)
			{
				this.Id = id;
				this.UserId = userId;
			}
		}
	}
}
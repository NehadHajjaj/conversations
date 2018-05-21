namespace Conversations.Core.Commands
{
	using System.Collections.Generic;
	using Conversations.Core.Commands.Core;
	using Conversations.Core.Domain;
	using Conversations.Core.Repositories;

	/// <summary>
	///     Adds a comment to an existing conversation.
	/// </summary>
	public class AddComment : RequestHandler<AddComment.Request, CommentIdentifier>
	{
		private readonly IConversationsRepository context;

		public AddComment(IConversationsRepository context)
		{
			this.context = context;
		}

		public override CommentIdentifier Handle(Request command)
		{
			var comment = this.context.AddConversationComment(command.Key, command.UserId, command.Text, command.DocumentIds,
				command.ParentCommentId);

			return new CommentIdentifier(comment.Id);
		}

		public class Request
		{
			public readonly string Key;
			public readonly int? ParentCommentId;
			public readonly string Text;
			public readonly int UserId;
			public List<int> DocumentIds;

			public Request(string key, int userId, string text, int? parentCommentId, List<int> documentIds)
			{
				this.Key = key;
				this.UserId = userId;
				this.Text = text;
				this.ParentCommentId = parentCommentId;
				this.DocumentIds = documentIds;
			}
		}
	}
}
namespace Conversations.Core.Commands
{
	using Conversations.Core.Commands.Core;
	using Conversations.Core.Domain;
	using Conversations.Core.Repositories;

	/// <summary>
	///     Get a comment from an existing conversation by comment id.
	/// </summary>
	public class GetCommentById : RequestHandler<GetCommentById.Request, CommentData>
	{
		private readonly IConversationsRepository context;

		public GetCommentById(IConversationsRepository context)
		{
			this.context = context;
		}

		public override CommentData Handle(Request command)
		{
			return this.context.GetComment(command.CommentId);
		}

		public class Request
		{
			public readonly int CommentId;

			public Request(int commentId)
			{
				this.CommentId = commentId;
			}
		}
	}
}
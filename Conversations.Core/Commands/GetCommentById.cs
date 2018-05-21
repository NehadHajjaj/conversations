using Conversations.Core.Commands.Core;
using Conversations.Core.Domain;
using Conversations.Core.Repositories;

namespace Conversations.Core.Commands
{
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
            return context.GetComment(command.CommentId);
        }

        public class Request
        {
            public readonly int CommentId;

            public Request(int commentId)
            {
                CommentId = commentId;
            }
        }
    }
}
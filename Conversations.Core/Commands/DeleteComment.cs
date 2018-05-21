using System;
using System.Linq;
using Conversations.Core.Commands.Core;
using Conversations.Core.Repositories;

namespace Conversations.Core.Commands
{
    /// <summary>
    ///     Delete a comment from an existing conversation.
    /// </summary>
    public class DeleteComment : RequestHandler<DeleteComment.Request>
    {
        private readonly IConversationsRepository context;

        public DeleteComment(IConversationsRepository context)
        {
            this.context = context;
        }

        public override void Handle(Request command)
        {
            var item = context.GetComment(command.CommentId);
            if (command.UserId == item.AuthorId && item.PostedOn > DateTime.UtcNow.AddHours(-12))
            {
                if (item.ConversationDocuments != null && item.ConversationDocuments.Any())
                    foreach (var doc in item.ConversationDocuments?.ToList())
                        context.RemoveConversationDocuments(doc);
                context.RemoveComments(item);
            }
        }

        public class Request
        {
            public readonly int CommentId;
            public readonly int UserId;

            public Request(int commentId, int userId)
            {
                CommentId = commentId;
                UserId = userId;
            }
        }
    }
}
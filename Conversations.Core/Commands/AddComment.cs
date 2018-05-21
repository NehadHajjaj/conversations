using System.Collections.Generic;
using System.Linq;
using Conversations.Core.Commands.Core;
using Conversations.Core.Domain;
using Conversations.Core.Repositories;

namespace Conversations.Core.Commands
{
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
            var documents = command.DocumentIds.Select(id => new ConversationDocument
                {
                    DocumentId = id
                })
                .ToList();
            var comment = context.AddConversationComment(command.Key, command.UserId, command.Text, documents,
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
                Key = key;
                UserId = userId;
                Text = text;
                ParentCommentId = parentCommentId;
                DocumentIds = documentIds;
            }
        }
    }
}
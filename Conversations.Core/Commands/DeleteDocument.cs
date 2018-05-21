using Conversations.Core.Commands.Core;
using Conversations.Core.Repositories;

namespace Conversations.Core.Commands
{
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
            context.DeleteConversationDocuments(command.Id, command.UserId);
        }

        public class Request
        {
            public readonly int Id;
            public readonly int UserId;

            public Request(int id, int userId)
            {
                Id = id;
                UserId = userId;
            }
        }
    }
}
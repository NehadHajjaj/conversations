using Conversations.Core.Commands.Core;
using Conversations.Core.Repositories;

namespace Conversations.Core.Commands
{
    /// <summary>
    ///     Un-Archives conversation making it editable.
    /// </summary>
    public class UnArchiveConversation : RequestHandler<UnArchiveConversation.Request>
    {
        private readonly IConversationsRepository context;

        public UnArchiveConversation(IConversationsRepository context)
        {
            this.context = context;
        }

        public override void Handle(Request command)
        {
            context.UnArchiveConversation(command.ConversationId);
        }

        public class Request
        {
            public Request(int conversationId)
            {
                ConversationId = conversationId;
            }

            public int ConversationId { get; }
        }
    }
}
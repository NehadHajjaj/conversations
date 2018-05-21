using Conversations.Core.Commands.Core;
using Conversations.Core.Repositories;

namespace Conversations.Core.Commands
{
    /// <summary>
    ///     Archives conversation making it read-only.
    /// </summary>
    public class ArchiveConversation : RequestHandler<ArchiveConversation.Request>
    {
        private readonly IConversationsRepository context;

        public ArchiveConversation(IConversationsRepository context)
        {
            this.context = context;
        }

        public override void Handle(Request command)
        {
            var conversation = context.GetConversation(command.ConversationId);
            context.ArchiveConversation(conversation, command.UserId);
        }

        public class Request
        {
            public Request(int conversationId, int userId)
            {
                ConversationId = conversationId;
                UserId = userId;
            }

            public int UserId { get; }
            public int ConversationId { get; }
        }
    }
}
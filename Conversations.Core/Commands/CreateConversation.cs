using Conversations.Core.Commands.Core;
using Conversations.Core.Domain;
using Conversations.Core.Repositories;

namespace Conversations.Core.Commands
{
    /// <summary>
    ///     Creates a conversation with the specified key. If conversation already exists,
    ///     then nothing happens.
    /// </summary>
    public class CreateConversation : RequestHandler<CreateConversation.Request, ConversationIdentifier>
    {
        private readonly IConversationsRepository context;

        public CreateConversation(IConversationsRepository context)
        {
            this.context = context;
        }

        public override ConversationIdentifier Handle(Request command)
        {
            var conversationId = this.context.GetConversation(command.ConversationKey)?.Id;

            if (conversationId == null)
            {
                var conversation = new ConversationData(command.ConversationKey);

                conversation = this.context.AddConversation(conversation.Id);

                conversationId = conversation.Id;
            }

            return new ConversationIdentifier(conversationId.Value);
        }

        public class Request
        {
            public string ConversationKey { get; set; }
        }
    }
}
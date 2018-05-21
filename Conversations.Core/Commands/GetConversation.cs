using Conversations.Core.Commands.Core;
using Conversations.Core.Domain;
using Conversations.Core.Repositories;

namespace Conversations.Core.Commands
{
    /// <summary>
    ///     Get conversation .
    /// </summary>
    public class GetConversation : RequestHandler<GetConversation.Request, ConversationData>
    {
        private readonly IConversationsRepository context;

        public GetConversation(IConversationsRepository context)
        {
            this.context = context;
        }

        public override ConversationData Handle(Request command)
        {
            return context.GetConversation(command.Id);
        }

        public class Request
        {
            public readonly object Id;

            public Request(object id)
            {
                Id = id;
            }
        }
    }
}
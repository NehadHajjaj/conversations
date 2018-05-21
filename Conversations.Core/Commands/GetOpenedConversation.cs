using System.Collections.Generic;
using Conversations.Core.Commands.Core;
using Conversations.Core.Domain;
using Conversations.Core.Repositories;

namespace Conversations.Core.Commands
{
    /// <summary>
    ///     Get list of opned conversation .
    /// </summary>
    public class GetConversations : RequestHandler<GetConversations.Request, IEnumerable<ConversationData>>
    {
        private readonly IConversationsRepository context;

        public GetConversations(IConversationsRepository context)
        {
            this.context = context;
        }

        public override IEnumerable<ConversationData> Handle(Request command)
        {
            return command.Opened ? context.GetOpenedConversations() : context.GetAllConversations();
        }

        public class Request
        {
            public bool Opened { get; set; }
        }
    }
}
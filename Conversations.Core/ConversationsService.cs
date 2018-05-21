using System.Collections.Generic;
using Conversations.Core.Commands;
using Conversations.Core.Domain;
using Conversations.Core.Repositories;

namespace Conversations.Core
{
    public class ConversationsService
    {
        private readonly AddComment addCommentHandler;
        private readonly ArchiveConversation archiveConversationHandler;
        private readonly IConversationsRepository context;
        private readonly CreateConversation createConversationHandler;
        private readonly DeleteComment deleteCommentHandler;
        private readonly DeleteDocument deleteDocumentHandler;
        private readonly GetCommentById getCommentByIdHandler;
        private readonly GetConversation getConversation;
        private readonly GetConversations getConversations;
        private readonly UnArchiveConversation unArchiveConversationHandler;

        public ConversationsService(IConversationsRepository context)
        {
            this.context = context;

            addCommentHandler = new AddComment(context);
            getCommentByIdHandler = new GetCommentById(context);
            deleteDocumentHandler = new DeleteDocument(context);
            deleteCommentHandler = new DeleteComment(context);
            createConversationHandler = new CreateConversation(context);
            archiveConversationHandler = new ArchiveConversation(context);
            unArchiveConversationHandler = new UnArchiveConversation(context);
            getConversation = new GetConversation(context);
            getConversations = new GetConversations(context);
        }


        public CommentIdentifier AddComment(AddComment.Request request)
        {
            return addCommentHandler.Handle(request);
        }

        public CommentData GetComment(GetCommentById.Request request)
        {
            return getCommentByIdHandler.Handle(request);
        }

        public void DeleteComment(DeleteComment.Request request)
        {
            deleteCommentHandler.Handle(request);
        }

        public void DeleteDocument(DeleteDocument.Request request)
        {
            deleteDocumentHandler.Handle(request);
        }

        public void ArchiveConversation(ArchiveConversation.Request request)
        {
            archiveConversationHandler.Handle(request);
        }

        public void UnArchiveConversation(UnArchiveConversation.Request request)
        {
            unArchiveConversationHandler.Handle(request);
        }

        public ConversationIdentifier CreateConversation(CreateConversation.Request request)
        {
            return createConversationHandler.Handle(request);
        }

        public ConversationData GetConversation(GetConversation.Request request)
        {
            return getConversation.Handle(request);
        }

        public IEnumerable<ConversationData> GetOpenedConversations()
        {
            return getConversations.Handle(new GetConversations.Request
            {
                Opened = true
            });
        }

        public IEnumerable<ConversationData> GetAllConversations()
        {
            return getConversations.Handle(new GetConversations.Request());
        }

        public int DeleteConversation(object key)
        {
            return context.DeleteConversation(key);
        }
    }
}
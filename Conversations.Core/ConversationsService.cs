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

			this.addCommentHandler = new AddComment(context);
			this.getCommentByIdHandler = new GetCommentById(context);
			this.deleteDocumentHandler = new DeleteDocument(context);
			this.deleteCommentHandler = new DeleteComment(context);
			this.createConversationHandler = new CreateConversation(context);
			this.archiveConversationHandler = new ArchiveConversation(context);
			this.unArchiveConversationHandler = new UnArchiveConversation(context);
			this.getConversation = new GetConversation(context);
			this.getConversations = new GetConversations(context);
        }


        public CommentIdentifier AddComment(AddComment.Request request)
        {
            return this.addCommentHandler.Handle(request);
        }

        public CommentData GetComment(GetCommentById.Request request)
        {
            return this.getCommentByIdHandler.Handle(request);
        }

        public void DeleteComment(DeleteComment.Request request)
        {
			this.deleteCommentHandler.Handle(request);
        }

        public void DeleteDocument(DeleteDocument.Request request)
        {
			this.deleteDocumentHandler.Handle(request);
        }

        public void ArchiveConversation(ArchiveConversation.Request request)
        {
			this.archiveConversationHandler.Handle(request);
        }

        public void UnArchiveConversation(UnArchiveConversation.Request request)
        {
			this.unArchiveConversationHandler.Handle(request);
        }

        public ConversationIdentifier CreateConversation(CreateConversation.Request request)
        {
            return this.createConversationHandler.Handle(request);
        }

        public ConversationData GetConversation(GetConversation.Request request)
        {
            return this.getConversation.Handle(request);
        }

        public IEnumerable<ConversationData> GetOpenedConversations()
        {
            return this.getConversations.Handle(new GetConversations.Request
            {
                Opened = true
            });
        }

        public IEnumerable<ConversationData> GetAllConversations()
        {
            return this.getConversations.Handle(new GetConversations.Request());
        }

        public void DeleteConversation(int key)
        {
            this.context.DeleteConversation(key);
        }
    }
}
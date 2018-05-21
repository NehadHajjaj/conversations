namespace Conversations.EntityFramework.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
	using Conversations.Core.Domain;
	using Conversations.Core.Repositories;
	using Conversations.EntityFramework.DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class ConversationRepository : IConversationsRepository
	{
        private readonly ConversationsDbContext context;

        public ConversationRepository(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConversationsDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            this.context = new ConversationsDbContext(optionsBuilder.Options);
        }

		public ConversationData AddConversation(int conversationId)
		{
			var conversation = this.context.Conversations
				.SingleOrDefault(c => c.Id == conversationId);

			this.context.Conversations.Add(conversation);
			this.context.SaveChanges();
			
			return conversation;
		}

		public ConversationData GetConversation(string key)
		{
			var conversation = this.context.Conversations
				.SingleOrDefault(c => c.Key == key);

			if (conversation == null)
			{
				var conversationData = new ConversationData(key);
			
				this.context.Conversations.Add(conversationData);
				this.context.SaveChanges();

				conversation = conversationData;
			}

			return conversation;
		}

		public ConversationData GetConversation(int conversationId)
		{
			var conversation = this.context.Conversations
				.SingleOrDefault(c => c.Id == conversationId);

			return conversation;
		}

		public IEnumerable<ConversationData> GetOpenedConversations()
		{
			var conversationData = this.context.Conversations.Where(a => !a.ArchivedOn.HasValue && a.Comments.Any()).OrderByDescending(a => a.Id).ToList();
			return conversationData;
		}

		public CommentData GetComment(int commentId)
		{
			var data = this.context.Comments.Find(commentId);
			return data;
		}

		public IEnumerable<ConversationData> GetAllConversations()
		{
			var conversationData = this.context.Conversations.Where(a => a.Comments.Any()).OrderByDescending(a => a.Id).ToList();
			return conversationData;
		}

		public void RemoveConversationDocument(int docId)
		{
			var item = this.context.ConversationDocuments.Include(a => a.CommentData).Single(s => s.DocumentId == docId);
			this.context.ConversationDocuments.Remove(item);
			this.context.SaveChanges();
		}

		public void RemoveComment(int commentId)
		{
			var comment = this.context.Comments.Find(commentId);
			this.context.Comments.Remove(comment);
			this.context.SaveChanges();
		}

		public void ArchiveConversation(int conversationId, int userId)
		{
			var conversation = this.context.Conversations.Find(conversationId);
			conversation.Archive(userId);
			this.context.SaveChanges();
		}

		public void UnArchiveConversation(int conversationId)
		{
			var conversation = this.context.Conversations.Find(conversationId);
			conversation.UnArchive();
			this.context.SaveChanges();
		}

		public CommentData AddConversationComment(string key, int userId, string text, List<int> documentIds, int? parentCommentId)
		{
			var conversation = this.context.Conversations
				.SingleOrDefault(c => c.Key == key);
			var documents = new List<ConversationDocument>();

			foreach (var id in documentIds)
			{
				var doc = new ConversationDocument
				{
					DocumentId = id
				};
				documents.Add(doc);
			}
			var comment = conversation.AddComment(userId, text, documents, parentCommentId);
			this.context.SaveChanges();

			return comment;
		}

		public void DeleteConversation(int conversationId)
		{
			var conversation = this.context.Conversations.SingleOrDefault(a => a.Id == conversationId);
			if (conversation != null)
			{
				var comments = this.context.Comments.Where(a => a.ConversationId == conversation.Id).ToList();
				foreach (var comment in comments)
				{
					this.context.Comments.Remove(comment);
				}
				this.context.Conversations.Remove(conversation);
				this.context.SaveChanges();
			}
		}
	}
}
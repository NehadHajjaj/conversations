namespace Conversations.EntityFramework.Repositories
{
	using System.Collections.Generic;
	using System.Linq;
	using Conversations.Core;
	using Conversations.Core.Domain;
	using Conversations.Core.Repositories;
	using Conversations.EntityFramework.DataAccess;
	using Microsoft.EntityFrameworkCore;

	public class ConversationRepository : IConversationsRepository
	{
		internal readonly ConversationsDbContext Dbcontext;

		public ConversationRepository(DataContext context)
		{
			this.Dbcontext = context.DbContext;
		}

		public ConversationData AddConversation(int conversationId)
		{
			var conversation = this.Dbcontext.Conversations
				.SingleOrDefault(c => c.Id == conversationId);

			this.Dbcontext.Conversations.Add(conversation);
			this.Dbcontext.SaveChanges();

			return conversation;
		}

		public ConversationData GetConversation(string key)
		{
			var conversation = this.Dbcontext.Conversations
				.SingleOrDefault(c => c.Key == key);

			if (conversation == null)
			{
				var conversationData = new ConversationData(key);

				this.Dbcontext.Conversations.Add(conversationData);
				this.Dbcontext.SaveChanges();

				conversation = conversationData;
			}

			return conversation;
		}

		public ConversationData GetConversation(int conversationId)
		{
			var conversation = this.Dbcontext.Conversations
				.SingleOrDefault(c => c.Id == conversationId);

			return conversation;
		}

		public IEnumerable<ConversationData> GetOpenedConversations()
		{
			var conversationData = this.Dbcontext.Conversations.Where(a => !a.ArchivedOn.HasValue && a.Comments.Any()).OrderByDescending(a => a.Id).ToList();
			return conversationData;
		}

		public CommentData GetComment(int commentId)
		{
			var data = this.Dbcontext.Comments.Find(commentId);
			return data;
		}

		public IEnumerable<ConversationData> GetAllConversations()
		{
			var conversationData = this.Dbcontext.Conversations.Where(a => a.Comments.Any()).OrderByDescending(a => a.Id).ToList();
			return conversationData;
		}

		public void RemoveConversationDocument(int docId)
		{
			var item = this.Dbcontext.ConversationDocuments.Include(a => a.CommentData).Single(s => s.DocumentId == docId);
			this.Dbcontext.ConversationDocuments.Remove(item);
			this.Dbcontext.SaveChanges();
		}

		public void RemoveComment(int commentId)
		{
			var comment = this.Dbcontext.Comments.Find(commentId);
			this.Dbcontext.Comments.Remove(comment);
			this.Dbcontext.SaveChanges();
		}

		public void ArchiveConversation(int conversationId, int userId)
		{
			var conversation = this.Dbcontext.Conversations.Find(conversationId);
			conversation.Archive(userId);
			this.Dbcontext.SaveChanges();
		}

		public void UnArchiveConversation(int conversationId)
		{
			var conversation = this.Dbcontext.Conversations.Find(conversationId);
			conversation.UnArchive();
			this.Dbcontext.SaveChanges();
		}

		public CommentData AddConversationComment(string key, int userId, string text, List<int> documentIds, int? parentCommentId)
		{
			var conversation = this.Dbcontext.Conversations
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
			this.Dbcontext.SaveChanges();

			return comment;
		}

		public void DeleteConversation(int conversationId)
		{
			var conversation = this.Dbcontext.Conversations.SingleOrDefault(a => a.Id == conversationId);
			if (conversation != null)
			{
				var comments = this.Dbcontext.Comments.Where(a => a.ConversationId == conversation.Id).ToList();
				foreach (var comment in comments)
				{
					this.Dbcontext.Comments.Remove(comment);
				}

				this.Dbcontext.Conversations.Remove(conversation);
				this.Dbcontext.SaveChanges();
			}
		}
	}
}
namespace Conversations.EntityFrameworkCore.Tests
{
	using System.Collections.Generic;
	using System.Linq;
	using Conversations.Core.Domain;
	using Conversations.Core.Repositories;
	using Conversations.EntityFramework.Repositories;
	using Xunit;

	[Collection(nameof(DatabaseCollectionFixture))]
	public class Program
    {
		public Program(DatabaseFixture dbFixture)
		{
			this.repository = new ConversationRepository(dbFixture.CreateDataContext());
		}

		private readonly IConversationsRepository repository;

		[Fact]
		public int? CreateConversation()
		{
			var conversationId = this.repository.GetConversation("EntityType:333")?.Id;
			return conversationId;
		}

		[Fact]
		public int? AddComment()
		{
			var conversationId = this.repository.GetConversation("EntityType:444")?.Id;
			var conversation = conversationId.HasValue ?this.repository.GetConversation(conversationId.Value):null;
			var comment = this.repository.AddConversationComment(conversation?.Key, 5,"nehad",new List<int>(), null);
			return comment.Id;

		}

		[Fact]
		public void ArchiveConversation()
		{
			var conversation = this.repository.GetConversation(1);
			this.repository.ArchiveConversation(conversation.Id, 5);

		}

		[Fact]
		public void UnArchiveConversation()
		{
			this.repository.UnArchiveConversation(1);
		}

		[Fact]
		public void DeleteComment()
		{
			var conversationId = this.repository.GetConversation("EntityType:444")?.Id;
			var conversation = conversationId.HasValue ? this.repository.GetConversation(conversationId.Value) : null;
			var comment = this.repository.AddConversationComment(conversation?.Key, 5, "nehad", new List<int>(), null);
			foreach (var doc in comment.ConversationDocuments?.ToList())
			{
				this.repository.RemoveConversationDocument(doc.DocumentId);
			}
			this.repository.RemoveComment(comment.Id);
		}
	}
}

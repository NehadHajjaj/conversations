namespace Conversations.Tests
{
	using System;
	using System.Linq;
	using FluentAssertions;
	using Microsoft.EntityFrameworkCore;
	using Xunit;

	public class ConversationDbContextTest
	{
		public ConversationDbContextTest()
		{
			this.options = new DbContextOptionsBuilder().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
			this.repository = this.GetDbContext();
		}

		private ConversationsDbContext<int, Conversation<int>, Comment<int>> GetDbContext()
		{
			return new ConversationsDbContext<int, Conversation<int>, Comment<int>>(this.options, "cnv");
		}

		private readonly ConversationsDbContext<int, Conversation<int>, Comment<int>> repository;
		private readonly DbContextOptions options;

		[Fact]
		public void CanAddTopLevelComment()
		{
			var conversation = this.repository.EnsureConversation("entity:1");
			conversation.AddComment(1, "lower ipsum");
			this.repository.SaveChanges();

			var ensured = this.repository.Conversations
				.AsNoTracking()
				.Include(t => t.Comments)
				.Single(t => t.Key == "entity:1");

			ensured.Comments.Count().Should().Be(1);
		}

		[Fact]
		public void CanEnsureExistingConversation()
		{
			// Create conversation.
			var c1 = new Conversation<int>(Guid.NewGuid().ToString());
			this.repository.Conversations.Add(c1);
			this.repository.SaveChanges();

			// Ensure existing conversation.
			var c2 = this.repository.EnsureConversation(c1.Key);
			c2.Should().NotBeNull();
			c2.Should().BeEquivalentTo(c1);
		}

		[Fact]
		public void CanEnsureNewConversation()
		{
			// Ensure existing conversation.
			var key = Guid.NewGuid().ToString();
			var c2 = this.repository.EnsureConversation(key);
			c2.Should().NotBeNull();
			c2.Key.Should().Be(key);
		}
	}
}
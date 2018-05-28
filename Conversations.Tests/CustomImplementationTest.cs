namespace Conversations.Tests
{
	using System;
	using System.Linq;
	using FluentAssertions;
	using Microsoft.EntityFrameworkCore;
	using Xunit;

	public class CustomImplementationTest
	{
		public CustomImplementationTest()
		{
			this.options = new DbContextOptionsBuilder().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
			this.repository = this.GetDbContext();
		}

		public class MyComment : Comment<string>
		{
			public string AnotherField { get; set; }
		}

		public class MyConversation : Conversation<string, MyComment>
		{
			public string Name { get; set; }
		}

		public class CustomConversations : ConversationsDbContext<string, MyConversation, MyComment>
		{
			public CustomConversations(DbContextOptions options, string schema) : base(options, schema)
			{
			}

			protected override void OnModelCreating(ModelBuilder builder)
			{
				base.OnModelCreating(builder);

				builder.Entity<MyConversation>()
					.Property(t => t.Name)
					.HasColumnName("Name");
			}
		}

		private CustomConversations GetDbContext()
		{
			return new CustomConversations(this.options, "cnv");
		}

		private readonly CustomConversations repository;
		private readonly DbContextOptions options;

		[Fact]
		public void CanExtendCommentEntity()
		{
			var conversation = this.repository.EnsureConversation("entity:1");
			var comment = conversation.AddComment("john", "text");
			comment.AnotherField = "test";
			this.repository.SaveChanges();

			var fromDb = this.GetDbContext().Conversations
				.Where(t => t.Key == "entity:1")
				.SelectMany(t => t.Comments)
				.Single();

			fromDb.AnotherField.Should().Be("test");
		}

		[Fact]
		public void CanExtendConversationEntity()
		{
			var conversation = this.repository.EnsureConversation("entity:1");
			conversation.Name = "test";
			this.repository.SaveChanges();

			var fromDb = this.GetDbContext().EnsureConversation("entity:1");
			fromDb.Name.Should().Be("test");
		}
	}
}
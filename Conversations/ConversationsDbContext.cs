namespace Conversations
{
	using System.Linq;
	using Conversations.Mappings;
	using Microsoft.EntityFrameworkCore;

	public class ConversationsDbContext<TAuthorKey, TConversation, TComment> : DbContext
		where TComment : Comment<TAuthorKey>, new()
		where TConversation : Conversation<TAuthorKey, TComment>, new()
	{
		private readonly string schema;

		public ConversationsDbContext(DbContextOptions options, string schema) : base(options)
		{
			this.schema = schema;
		}

		public virtual DbSet<TComment> Comments { get; set; }
		public virtual DbSet<TConversation> Conversations { get; set; }

		public void DeleteConversation(int conversationId)
		{
			var conversation = this.Conversations.SingleOrDefault(a => a.Id == conversationId);
			if (conversation != null)
			{
				var comments = this.Comments.Where(a => a.ConversationId == conversation.Id).ToList();
				foreach (var comment in comments)
				{
					this.Comments.Remove(comment);
				}

				this.Conversations.Remove(conversation);
				this.SaveChanges();
			}
		}

		public TConversation EnsureConversation(string key)
		{
			var conversation = this.Conversations.SingleOrDefault(c => c.Key == key);

			if (conversation == null)
			{
				conversation = new TConversation();
				conversation.ChangeKey(key);

				this.Conversations.Add(conversation);
				this.SaveChanges();
			}

			return conversation;
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.HasDefaultSchema(this.schema);
			builder.ApplyConfiguration(new CommentMap<TAuthorKey>(this.schema));
			builder.ApplyConfiguration(new ConversationMap<TAuthorKey, TComment>(this.schema));
		}
	}
}
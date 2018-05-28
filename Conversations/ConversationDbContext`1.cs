namespace Conversations
{
	using Microsoft.EntityFrameworkCore;

	public class ConversationsDbContext<TAuthorKey>
		: ConversationsDbContext<
			TAuthorKey,
			Conversation<TAuthorKey, Comment<TAuthorKey>>,
			Comment<TAuthorKey>>
	{
		public ConversationsDbContext(DbContextOptions options, string schema) : base(options, schema)
		{
		}
	}
}
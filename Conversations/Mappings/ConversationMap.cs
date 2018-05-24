namespace Conversations.Mappings
{
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	internal class ConversationMap<TAuthorKey> : IEntityTypeConfiguration<Conversation<TAuthorKey>>
	{
		private readonly string schema;

		public ConversationMap(string schema)
		{
			this.schema = schema;
		}

		public void Configure(EntityTypeBuilder<Conversation<TAuthorKey>> entity)
		{
			entity.ToTable("Conversation", this.schema);
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
			entity.Property(t => t.Key).HasColumnName("Key").HasMaxLength(Conversation<TAuthorKey>.KeyMaxLength).IsUnicode(false).IsRequired();

			entity.HasMany(t => t.Comments)
				.WithOne(t => t.Conversation)
				.HasForeignKey(t => t.ConversationId);

			entity.EnumerableNavigationProperty(
				nameof(Conversation<TAuthorKey>.Comments),
				Conversation<TAuthorKey>.CommentsFieldName);
		}
	}
}
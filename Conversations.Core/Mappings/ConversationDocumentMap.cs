namespace Conversations.EntityFramework.Mappings
{
	using Conversations.Core.Domain;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	internal class ConversationDocumentMap : IEntityTypeConfiguration<ConversationDocument>
	{
		public void Configure(EntityTypeBuilder<ConversationDocument> entity)
		{
			entity.ToTable("ConversationDocument");
			entity.HasKey(t => t.Id);
			entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
			entity.Property(t => t.CommentDataId).HasColumnName("CommentDataId");

			entity.HasOne(t => t.CommentData)
				.WithMany(t => t.ConversationDocuments)
				.HasForeignKey(t => t.CommentDataId);
		}
	}
}
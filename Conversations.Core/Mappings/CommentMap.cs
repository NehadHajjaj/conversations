namespace Conversations.Core.Mappings
{
	using Conversations.Core.Domain;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	internal class CommentMap : IEntityTypeConfiguration<CommentData>
    {
        public void Configure(EntityTypeBuilder<CommentData> entity)
        {
           
            entity.ToTable("Comment");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.AuthorId).HasColumnName("AuthorId");
            entity.Property(t => t.PostedOn).HasColumnName("PostedOn");
            entity.Property(t => t.Text).HasColumnName("Text").IsUnicode(true).IsRequired();
            entity.Property(t => t.ParentId).HasColumnName("ParentId").HasAnnotation("IX_Comment_ParentId",true);
            entity.Property(t => t.ConversationId).HasColumnName("ConversationId").HasAnnotation("IX_Comment_ConversationId",true);
            entity.Property(t => t.CorrelationId).HasColumnName("CorrelationId").HasAnnotation("UQ_Comment_CorrelationId", true);
            entity.Ignore(t => t.Identifier);

            entity.HasOne(t => t.ConversationData)
                .WithMany(t => t.Comments)
                .HasForeignKey(t => t.ConversationId);

            entity.HasMany(t => t.Children)
                .WithOne()
                .HasForeignKey(t => t.ParentId);
        }
        
    }
}
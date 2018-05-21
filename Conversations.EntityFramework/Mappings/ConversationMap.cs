namespace Conversations.EntityFramework.Mappings
{
    using Conversations.Core;
	using Conversations.Core.Domain;
	using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ConversationMap : IEntityTypeConfiguration<ConversationData>
    {
        public void Configure(EntityTypeBuilder<ConversationData> entity)
        {
            entity.ToTable("Conversation");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).HasColumnName("Id").UseSqlServerIdentityColumn();
            entity.Property(t => t.CreatedOn).HasColumnName("CreatedOn");
            entity.Property(t => t.ArchivedOn).HasColumnName("ArchivedOn");
            entity.Property(t => t.Key).HasColumnName("Key").HasMaxLength(80).IsUnicode(false).IsRequired();
            entity.HasMany(t => t.Comments).WithOne(t => t.ConversationData).HasForeignKey(t => t.ConversationId);
            entity.Ignore(t => t.Reference);
            entity.Ignore(t => t.Identifier);
        }
       
          
    }
}
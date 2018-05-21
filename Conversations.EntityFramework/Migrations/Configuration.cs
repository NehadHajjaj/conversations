namespace Conversations.EntityFramework.Migrations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
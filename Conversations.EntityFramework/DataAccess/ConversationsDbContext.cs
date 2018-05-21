namespace Conversations.EntityFramework.DataAccess
{
	using Conversations.Core.Domain;
	using Conversations.EntityFramework.Mappings;
    using Microsoft.EntityFrameworkCore;

    public class ConversationsDbContext : DbContext
	{
        public ConversationsDbContext()
	    	: base(new DbContextOptionsBuilder().UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=conv;Trusted_Connection=True;MultipleActiveResultSets=true").Options)
	    {
        }

        public ConversationsDbContext(DbContextOptions options) : base(options)
	    {
	    }

	    public virtual DbSet<CommentData> Comments { get; set; }
	    public virtual DbSet<ConversationData> Conversations { get; set; }
	    public virtual DbSet<ConversationDocument> ConversationDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
	    {
	        base.OnModelCreating(builder);

	        builder.HasDefaultSchema("cnv");
	        builder.ApplyConfiguration(new CommentMap());
	        builder.ApplyConfiguration(new ConversationMap());
	        builder.ApplyConfiguration(new ConversationDocumentMap());
        }


    }
}
namespace Conversations.Core.DataAccess
{
	using Conversations.Core.Domain;
	using Conversations.Core.Mappings;
	using Microsoft.EntityFrameworkCore;

	public class ConversationsDbContext : DbContext
	{
		private const string DefaultConnectionString =
			"Server=(localdb)\\mssqllocaldb;Database=convy;Trusted_Connection=True;MultipleActiveResultSets=true";

		private readonly string schema;

		public ConversationsDbContext()
			: this(new DbContextOptionsBuilder().UseSqlServer(DefaultConnectionString).Options, "cnv")
		{
		}
      
		public ConversationsDbContext(DbContextOptions options, string schema) : base(options)
		{
			this.schema = schema;
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
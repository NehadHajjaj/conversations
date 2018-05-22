namespace Conversations.EntityFramework
{
	using Conversations.EntityFramework.DataAccess;
	using Microsoft.EntityFrameworkCore;

	public class DataContext
    {
		/// <summary>
		/// Instantiates a new instance of the DataContext class.
		/// </summary>
		public DataContext(DbContextOptions options, string schema = "cnv")
		{
			this.DbContext = new ConversationsDbContext(options, schema);
		}

		// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
		internal ConversationsDbContext DbContext { get; private set; }

		/// <summary>
		/// Runs <see cref="RelationalDatabaseFacadeExtensions.Migrate"/> underlying <see cref="Microsoft.EntityFrameworkCore.DbContext"/>,
		/// to make sure database exists and all migrations are run.
		/// </summary>
		public void MigrateDatabase()
		{
			this.DbContext.Database.Migrate();
		}
	}
}

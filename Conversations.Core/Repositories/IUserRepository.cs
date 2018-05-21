namespace Conversations.Core.Repositories
{
	public interface IUserRepository<out TUser>
	{
		TUser GetUser(int id);
	}
}
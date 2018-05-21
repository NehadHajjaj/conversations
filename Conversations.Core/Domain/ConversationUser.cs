namespace Conversations.Core.Domain
{
	public class ConversationUser
	{
		public int Id { get; set; }
		public byte[] Photo { get; set; }
		public string DisplayName { get; set; }
	}
}
namespace Conversations.Core.Domain
{
	public interface IIdentifier<out T>
	{
		T Value { get; }
	}
}
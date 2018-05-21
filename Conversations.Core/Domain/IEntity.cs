namespace Conversations.Core.Domain
{
	internal interface IEntity<out TIdentifier, T> where TIdentifier : IIdentifier<T>
	{
		TIdentifier Identifier { get; }
	}
}
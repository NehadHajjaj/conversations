namespace Conversations.Core.Commands.Core
{
	internal interface IRequestHandler<in TRequest, out TResponse>
	{
		TResponse Handle(TRequest command);
	}

	internal interface IRequestHandler<in TRequest>
	{
		void Handle(TRequest command);
	}
}
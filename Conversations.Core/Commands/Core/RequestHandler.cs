namespace Conversations.Core.Commands.Core
{
	public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
	{
		public abstract TResponse Handle(TRequest command);
	}

	public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest>
	{
		public abstract void Handle(TRequest command);
	}
}
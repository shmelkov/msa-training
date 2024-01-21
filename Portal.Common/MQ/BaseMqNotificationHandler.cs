using MassTransit;
using MediatR;

namespace Portal.Common.MQ
{
    public abstract class BaseMqNotificationHandler<T> : INotificationHandler<T> where T : INotification
    {
        private readonly IPublishEndpoint _publishEndpoint;

        protected BaseMqNotificationHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public virtual Task Handle(T notification, CancellationToken cancellationToken)
        {
            return _publishEndpoint.Publish(notification, cancellationToken);
        }
    }
}

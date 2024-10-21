using CHUNO.Framework.Domain.Events;
using MediatR;

namespace CHUNO.Framework.Infrastructure.Common
{
    internal class DefaultEventDispatcher : IEventDispatcher
    {
        private readonly IMediator _mediator;
        public DefaultEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return _mediator.Publish(new EventNotification(domainEvent), cancellationToken);
        }
    }
}

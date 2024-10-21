
using CHUNO.Framework.Domain.Events;
using CHUNO.Framework.Infrastructure.Common;
using MediatR;

namespace CHUNO.Framework.Infrastructure.Messaging
{
    /// <summary>
    /// Represents a domain event handler interface.
    /// </summary>
    /// <typeparam name="TDomainEvent">The domain event type.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : EventNotification
    {
    }
}
using MediatR;

namespace CHUNO.Framework.Infrastructure.Messaging
{
    /// <summary>
    /// Represents the event interface.
    /// </summary>
    public interface IEvent : INotification
    {
    }
}
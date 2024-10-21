using MediatR;

namespace CHUNO.Framework.Infrastructure.Messaging
{
    /// <summary>
    /// Represents the marker interface for an integration event.
    /// </summary>
    public interface IIntegrationEvent : INotification
    {
    }
}

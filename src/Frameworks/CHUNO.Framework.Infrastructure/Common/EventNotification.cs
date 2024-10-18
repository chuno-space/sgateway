using CHUNO.Framework.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUNO.Framework.Infrastructure.Common
{
    public class EventNotification : INotification
    {
        public IDomainEvent DomainEvent { get; private set; }
        public EventNotification(IDomainEvent domainEvent) {
            DomainEvent = domainEvent;
        }
    }
}

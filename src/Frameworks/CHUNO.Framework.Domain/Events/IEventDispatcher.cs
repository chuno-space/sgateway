using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CHUNO.Framework.Domain.Events
{
    public interface IEventDispatcher
    {
        public Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}

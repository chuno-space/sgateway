using CHUNO.Framework.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUNO.Framework.Domain.Primitives
{
    public abstract class BaseAggregateRoot : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
    {
        protected BaseAggregateRoot(Guid id)
           : base(id)
        {
        }
        protected BaseAggregateRoot()
        {
        }

        // interface IAuditableEntity
        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        // interface ISoftDeletableEntity
        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }
    }
}

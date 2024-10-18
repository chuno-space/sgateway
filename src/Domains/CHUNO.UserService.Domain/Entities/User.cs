using CHUNO.Framework.Domain.Abstractions;
using CHUNO.Framework.Domain.Primitives;

namespace CHUNO.UserService.Domain.Entities
{
    public sealed class User : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
    {
        // interface IAuditableEntity
        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        // interface ISoftDeletableEntity
        /// <inheritdoc />
        public DateTime? DeletedOnUtc { get; }

        /// <inheritdoc />
        public bool Deleted { get; }
    }
}

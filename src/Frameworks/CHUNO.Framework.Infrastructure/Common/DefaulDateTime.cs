using CHUNO.Framework.Core.Intefaces;
namespace CHUNO.Framework.Infrastructure.Common
{
    internal sealed class DefaultDateTime : IDateTime
    {
        /// <inheritdoc />
        public DateTime UtcNow => DateTime.UtcNow;
    }
}

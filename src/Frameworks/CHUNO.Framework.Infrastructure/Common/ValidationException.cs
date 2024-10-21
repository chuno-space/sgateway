using CHUNO.Framework.Domain.Primitives;
using FluentValidation.Results;

namespace CHUNO.Framework.Infrastructure.Common
{
    public sealed class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class.
        /// </summary>
        /// <param name="failures">The collection of validation failures.</param>
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("One or more validation failures has occurred.") =>
            Errors = failures
                .Distinct()
                .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
                .ToList();

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        public IReadOnlyCollection<Error> Errors { get; }
    }
}

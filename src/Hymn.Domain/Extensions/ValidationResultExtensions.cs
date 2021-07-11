using FluentValidation.Results;

namespace Hymn.Domain.Extensions
{
    public static class ValidationResultExtensions
    {
        public static ValidationResult Plus(this ValidationResult a, ValidationResult b)
        {
            b.Errors.ForEach(e => a.Errors.Add(e));
            return a;
        }
    }
}
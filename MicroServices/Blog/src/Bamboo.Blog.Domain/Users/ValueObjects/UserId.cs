﻿using SharedKernel.Domain;
using System.Diagnostics.CodeAnalysis;

namespace Bamboo.Users.ValueObjects
{
    /// <summary>
    /// User Id
    /// </summary>
    public class UserId(Guid value) : ValueObject, IParsable<UserId>
    {
        /// <summary>
        /// 值
        /// </summary>
        public Guid Value { get; } = value;

        /// <inheritdoc/>
        public static UserId Parse(string s, IFormatProvider? provider)
        {
            return new UserId(Guid.Parse(s));
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out UserId result)
        {
            if (Guid.TryParse(s, out var value))
            {
                result = new UserId(value);
                return true;
            }

            result = null;
            return false;
        }

        /// <inheritdoc/>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}

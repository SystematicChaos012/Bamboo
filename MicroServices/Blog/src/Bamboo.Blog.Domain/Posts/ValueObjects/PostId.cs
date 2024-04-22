using SharedKernel.Domain;
using System.Diagnostics.CodeAnalysis;

namespace Bamboo.Posts.ValueObjects
{
    /// <summary>
    /// Post Id
    /// </summary>
    public sealed class PostId(Guid value) : ValueObject<Guid>(value), IParsable<PostId>
    {
        /// <inheritdoc/>
        public static PostId Parse(string s, IFormatProvider? provider)
        {
            return new PostId(Guid.Parse(s));
        }

        /// <inheritdoc/>
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out PostId result)
        {
            if (Guid.TryParse(s, out var value))
            {
                result = new PostId(value);
                return true;
            }

            result = null;
            return false;
        }
    }
}

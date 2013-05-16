using System;

namespace DownMarker.Core
{
    /// <summary>
    /// Immutable representation of a string change.
    /// </summary>
    /// <remarks>
    /// The string change is represented as the replacement of a substring at
    /// a given offset by another substring. This way we can represent any
    /// string change with an object whose size is proportional to the change.
    /// The stored information can also be used to undo the change.
    /// </remarks>
    public sealed class StringChange
    {
        private readonly int offset;
        private readonly string original;
        private readonly string replacement;

        /// <summary>
        /// Calculates a <see cref="StringChange"/> which transforms
        /// <paramref name="s1"/> into <paramref name="s2"/>.
        /// </summary>
        public static StringChange GetStringChange(string s1, string s2)
        {
            int offset = 0;
            int max = Math.Min(s1.Length, s2.Length);
            while ((offset < max) && (s1[offset] == s2[offset]))
            {
                offset++;
            }

            int rightSideMatches = 0;
            max = Math.Min(s1.Length - offset, s2.Length - offset);
            while ((rightSideMatches < max)
                && (s1[s1.Length - rightSideMatches - 1] == s2[s2.Length - rightSideMatches - 1]))
            {
                rightSideMatches++;
            }

            return new StringChange(
                offset,
                s1.Substring(offset, s1.Length - offset - rightSideMatches),
                s2.Substring(offset, s2.Length - offset - rightSideMatches));
        }

        /// <summary>
        /// Creates a new <see cref="StringChange"/> which changes the
        /// original substring at a given offset by another substring.
        /// </summary>
        public StringChange(int offset, string original, string replacement)
        {
            this.offset = offset;
            this.original = original;
            this.replacement = replacement;
        }

        /// <summary>
        /// Applies this change to the given string.
        /// </summary>
        public string Do(string s)
        {
            return s.ReplaceSubstring(offset, original.Length, replacement);
        }

        /// <summary>
        /// Undoes this change on the given string.
        /// </summary>
        public string Undo(string s)
        {
            return s.ReplaceSubstring(offset, replacement.Length, original);
        }
    }
}

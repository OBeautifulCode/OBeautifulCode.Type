// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncodingKind.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// Specifies the kind of encoding.
    /// </summary>
    public enum EncodingKind
    {
        /// <summary>
        /// Encoding for the ASCII (7-bit) character set.
        /// </summary>
        Ascii,

        /// <summary>
        /// The encoding for the UTF-7 format.
        /// </summary>
        Utf7,

        /// <summary>
        /// The encoding for the UTF-8 encoding.
        /// </summary>
        Utf8,

        /// <summary>
        /// The encoding for the UTF-16 format using the little endian byte order.
        /// </summary>
        /// <remarks>
        /// This is also called 'Unicode' and 'UCS-2 little endian'.
        /// </remarks>
        Utf16LittleEndian,

        /// <summary>
        /// The encoding for the UTF-16 format using the big endian byte order.
        /// </summary>
        Utf16BigEndian,

        /// <summary>
        /// The encoding for the UTF-16 format using the little endian byte order.
        /// </summary>
        Utf32LittleEndian,

        /// <summary>
        /// The encoding for the UTF-16 format using the big endian byte order.
        /// </summary>
        Utf32BigEndian,

        /// <summary>
        /// The encoding for the Western European (ISO-8559-1) format.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Iso", Justification = ObcSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        WesternEuropeanIso,
    }
}

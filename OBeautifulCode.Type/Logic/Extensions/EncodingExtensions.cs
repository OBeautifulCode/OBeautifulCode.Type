// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncodingExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System;
    using System.Text;

    using static System.FormattableString;

    /// <summary>
    /// Extension methods related to <see cref="Encoding"/>.
    /// </summary>
    public static class EncodingExtensions
    {
        private const int WesternEuropeanIsoCodePage = 28591;

        private const int Utf32BigEndianCodePage = 12001;

        /// <summary>
        /// Converts an <see cref="EncodingKind"/> to an <see cref="Encoding"/>.
        /// </summary>
        /// <param name="encodingKind">The encoding kind.</param>
        /// <returns>
        /// The <see cref="Encoding"/> corresponding to the specified <see cref="EncodingKind"/>.
        /// </returns>
        public static Encoding ToEncoding(
            this EncodingKind encodingKind)
        {
            switch (encodingKind)
            {
                case EncodingKind.Ascii:
                    return Encoding.ASCII;
                case EncodingKind.Utf7:
                    return Encoding.UTF7;
                case EncodingKind.Utf8:
                    return Encoding.UTF8;
                case EncodingKind.Utf16LittleEndian:
                    return Encoding.Unicode;
                case EncodingKind.Utf16BigEndian:
                    return Encoding.BigEndianUnicode;
                case EncodingKind.Utf32LittleEndian:
                    return Encoding.UTF32;
                case EncodingKind.Utf32BigEndian:
                    return Encoding.GetEncoding(Utf32BigEndianCodePage);
                case EncodingKind.WesternEuropeanIso:
                    return Encoding.GetEncoding(WesternEuropeanIsoCodePage);
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(encodingKind)} is not supported: {encodingKind}."));
            }
        }

        /// <summary>
        /// Converts an <see cref="Encoding"/> to an <see cref="EncodingKind"/>.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// The <see cref="EncodingKind"/> corresponding to the specified <see cref="Encoding"/>.
        /// </returns>
        public static EncodingKind ToEncodingKind(
            this Encoding encoding)
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            int codePage = encoding.CodePage;

            if (codePage == Encoding.ASCII.CodePage)
            {
                return EncodingKind.Ascii;
            }
            else if (codePage == Encoding.UTF7.CodePage)
            {
                return EncodingKind.Utf7;
            }
            else if (codePage == Encoding.UTF8.CodePage)
            {
                return EncodingKind.Utf8;
            }
            else if (codePage == Encoding.Unicode.CodePage)
            {
                return EncodingKind.Utf16LittleEndian;
            }
            else if (codePage == Encoding.BigEndianUnicode.CodePage)
            {
                return EncodingKind.Utf16BigEndian;
            }
            else if (codePage == Encoding.UTF32.CodePage)
            {
                return EncodingKind.Utf32LittleEndian;
            }
            else if (codePage == Utf32BigEndianCodePage)
            {
                return EncodingKind.Utf32BigEndian;
            }
            else if (codePage == WesternEuropeanIsoCodePage)
            {
                return EncodingKind.WesternEuropeanIso;
            }
            else
            {
                throw new NotSupportedException(Invariant($"An encoding with this code page is not supported: {codePage}."));
            }
        }
    }
}

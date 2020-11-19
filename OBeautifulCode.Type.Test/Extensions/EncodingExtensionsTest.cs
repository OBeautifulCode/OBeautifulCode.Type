// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncodingExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System.Linq;
    using System.Text;

    using FluentAssertions;

    using Xunit;

    public static class EncodingExtensionsTest
    {
        [Fact]
        public static void ToEncoding___Should_roundtrip_an_encoding___When_encoding_is_converted_to_EncodingKind_using_ToEncodingKind()
        {
            // Arrange
            var expected = new[]
            {
                Encoding.ASCII,
                Encoding.UTF7,
                Encoding.UTF8,
                Encoding.Unicode,
                Encoding.BigEndianUnicode,
                Encoding.UTF32,
                Encoding.GetEncoding("utf-32BE"),
                Encoding.GetEncoding("iso-8859-1"),
            };

            var encodingKinds = expected.Select(_ => _.ToEncodingKind()).ToList();

            // Act
            var actual = encodingKinds.Select(_ => _.ToEncoding()).ToArray();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public static void ToEncodingKind___Should_roundtrip_an_EncodingKind___When_EncodingKind_is_converted_to_Encoding_using_ToEncoding()
        {
            // Arrange
            var expected = new[]
            {
                EncodingKind.Ascii,
                EncodingKind.Utf7,
                EncodingKind.Utf8,
                EncodingKind.Utf16LittleEndian,
                EncodingKind.Utf16BigEndian,
                EncodingKind.Utf32LittleEndian,
                EncodingKind.Utf32BigEndian,
                EncodingKind.WesternEuropeanIso,
            };

            var encodings = expected.Select(_ => _.ToEncoding()).ToList();

            // Act
            var actual = encodings.Select(_ => _.ToEncodingKind()).ToArray();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}

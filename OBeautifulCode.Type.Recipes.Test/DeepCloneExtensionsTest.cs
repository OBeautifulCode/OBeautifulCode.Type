// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeepCloneExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Recipes.Test
{
    using System;
    using System.Linq;

    using FakeItEasy;

    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    public static class DeepCloneExtensionsTest
    {
        [Fact]
        public static void DeepClone_String___Should_deep_clone_a_String___When_called()
        {
            // Arrange
            var expected = A.Dummy<string>();

            // Act
            var actual = expected.DeepClone();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);

            // the same referenced IS returned
            // actual.AsTest().Must().NotBeSameReferenceAs(expected);
        }

        [Fact]
        public static void DeepClone_Version___Should_deep_clone_a_Version___When_called()
        {
            // Arrange
            var expected = A.Dummy<Version>();

            // Act
            var actual = expected.DeepClone();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
            actual.AsTest().Must().NotBeSameReferenceAs(expected);
        }

        [Fact]
        public static void DeepClone_Uri___Should_deep_clone_a_uri___When_called()
        {
            // Arrange
            var expected = new[]
            {
                new Uri("http://duckduckgo.com/search"),
                new Uri("http://duckduckgo.com/search/"),
                new Uri("http://duckduckgo.com/search/whatever.html"),
                new Uri("http://duckduckgo.com/search/whatever.html?id=monkey&name=something"),
                new Uri("http://duckduckgo.com/search.html#anchor"),
                new Uri("c:\\my-folder\\whatever.txt"),
                new Uri("c:\\my-folder\\whatever\\"),
                new Uri("/Skins/MainSkin.xaml", UriKind.Relative),
                new Uri("/Skins/MainSkin.xaml#anchor", UriKind.Relative),
            };

            var expectedToStrings = expected.Select(_ => _.ToString()).ToList();

            // Act
            var actuals = expected.Select(_ => _.DeepClone()).ToArray();
            var actualToStrings = expected.Select(_ => _.ToString()).ToList();

            // Assert
            for (var x = 0; x < expected.Length; x++)
            {
                actuals[x].AsTest().Must().BeEqualTo(expected[x]);
                actuals[x].AsTest().Must().NotBeSameReferenceAs(expected[x]);
                actualToStrings[x].AsTest().Must().BeEqualTo(expectedToStrings[x]);
            }
        }
    }
}

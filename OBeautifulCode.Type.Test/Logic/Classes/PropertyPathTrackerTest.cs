// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyPathTrackerTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using Xunit;

    public static class PropertyPathTrackerTest
    {
        [Fact]
        public static void SegmentSeparator___Should_return_segmentSeparator_passed_to_constructor___When_called()
        {
            // Arrange
            var expected = A.Dummy<string>();

            var systemUnderTest = new PropertyPathTracker(expected);

            // Act
            var actual = systemUnderTest.SegmentSeparator;

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void FullPath___Should_return_the_full_path_to_the_property___When_called()
        {
            // Arrange
            var separator = "%";

            var systemUnderTest1 = new PropertyPathTracker(separator);

            var systemUnderTest2 = new PropertyPathTracker(separator);
            systemUnderTest2.Push("segment-1");

            var systemUnderTest3 = new PropertyPathTracker(separator);
            systemUnderTest3.Push("segment-1");
            systemUnderTest3.Push("segment-2");

            var systemUnderTest4 = new PropertyPathTracker(separator);
            systemUnderTest4.Push("segment-1");
            systemUnderTest4.Push("segment-2");
            systemUnderTest4.Push("segment-3");

            var systemUnderTest5 = new PropertyPathTracker(separator);
            systemUnderTest5.Push("segment-1");
            systemUnderTest5.Push("segment-2");
            using (systemUnderTest5.Push("segment-3"))
            {
            }

            var systemUnderTest6 = new PropertyPathTracker(separator);
            systemUnderTest6.Push("segment-1");
            using (systemUnderTest6.Push("segment-2"))
            {
                using (systemUnderTest6.Push("segment-3"))
                {
                }
            }

            var systemUnderTest7 = new PropertyPathTracker(separator);
            using (systemUnderTest7.Push("segment-1"))
            {
                using (systemUnderTest7.Push("segment-2"))
                {
                    using (systemUnderTest7.Push("segment-3"))
                    {
                    }
                }
            }

            var systemUnderTest8 = new PropertyPathTracker(separator);
            using (systemUnderTest8.Push("segment-1"))
            {
            }

            var systemsUnderTest = new[]
            {
                systemUnderTest1,
                systemUnderTest2,
                systemUnderTest3,
                systemUnderTest4,
                systemUnderTest5,
                systemUnderTest6,
                systemUnderTest7,
                systemUnderTest8,
            };

            var expected = new[]
            {
                string.Empty,
                "segment-1",
                "segment-1%segment-2",
                "segment-1%segment-2%segment-3",
                "segment-1%segment-2",
                "segment-1",
                string.Empty,
                string.Empty,
            };

            // Act
            var actual = systemsUnderTest.Select(_ => _.FullPath).ToArray();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }

        [Fact]
        public static void HasSegments___Should_return_expected_value___When_called()
        {
            // Arrange
            var systemUnderTest1 = new PropertyPathTracker();

            var systemUnderTest2 = new PropertyPathTracker();
            systemUnderTest2.Push(A.Dummy<string>());

            var systemUnderTest3 = new PropertyPathTracker();
            systemUnderTest3.Push(A.Dummy<string>());
            systemUnderTest3.Push(A.Dummy<string>());

            var systemUnderTest4 = new PropertyPathTracker();
            systemUnderTest4.Push(A.Dummy<string>());
            systemUnderTest4.Push(A.Dummy<string>());
            systemUnderTest4.Push(A.Dummy<string>());

            var systemUnderTest5 = new PropertyPathTracker();
            systemUnderTest5.Push(A.Dummy<string>());
            systemUnderTest5.Push(A.Dummy<string>());
            using (systemUnderTest5.Push(A.Dummy<string>()))
            {
            }

            var systemUnderTest6 = new PropertyPathTracker();
            systemUnderTest6.Push(A.Dummy<string>());
            using (systemUnderTest6.Push(A.Dummy<string>()))
            {
                using (systemUnderTest6.Push(A.Dummy<string>()))
                {
                }
            }

            var systemUnderTest7 = new PropertyPathTracker();
            using (systemUnderTest7.Push(A.Dummy<string>()))
            {
                using (systemUnderTest7.Push(A.Dummy<string>()))
                {
                    using (systemUnderTest7.Push(A.Dummy<string>()))
                    {
                    }
                }
            }

            var systemUnderTest8 = new PropertyPathTracker();
            using (systemUnderTest8.Push(A.Dummy<string>()))
            {
            }

            var systemsUnderTest = new[]
            {
                systemUnderTest1,
                systemUnderTest2,
                systemUnderTest3,
                systemUnderTest4,
                systemUnderTest5,
                systemUnderTest6,
                systemUnderTest7,
                systemUnderTest8,
            };

            var expected = new[]
            {
                false,
                true,
                true,
                true,
                true,
                true,
                false,
                false,
            };

            // Act
            var actual = systemsUnderTest.Select(_ => _.HasSegments).ToArray();

            // Assert
            actual.AsTest().Must().BeEqualTo(expected);
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    using static System.FormattableString;

    using TypeExtensions = OBeautifulCode.Type.Recipes.TypeExtensions;

    public static class AssemblyExtensionsTest
    {
        [Fact]
        public static void GetEnumTypes___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => AssemblyExtensions.GetEnumTypes(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("assembly");
        }

        [Fact]
        public static void GetEnumTypes___Should_return_enum_types___When_called()
        {
            // Arrange, Act
            var actual = ProjectInfo.Assembly.GetEnumTypes();

            // Assert
            actual.Should().Contain(typeof(PublicEnum));
            actual.Should().Contain(typeof(InternalEnum));
            actual.Should().NotContain(typeof(IPublicInterface));
            actual.Should().NotContain(typeof(IInternalInterface));
        }

        [Fact]
        public static void GetPublicEnumTypes___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => AssemblyExtensions.GetPublicEnumTypes(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("assembly");
        }

        [Fact]
        public static void GetPublicEnumTypes___Should_return_public_enum_types___When_called()
        {
            // Arrange, Act
            var actual = ProjectInfo.Assembly.GetPublicEnumTypes();

            // Assert
            actual.Should().Contain(typeof(PublicEnum));
            actual.Should().NotContain(typeof(InternalEnum));
            actual.Should().NotContain(typeof(IPublicInterface));
            actual.Should().NotContain(typeof(IInternalInterface));
        }

        [Fact]
        public static void GetInterfaceTypes___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => AssemblyExtensions.GetInterfaceTypes(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("assembly");
        }

        [Fact]
        public static void GetInterfaceTypes___Should_return_interface_types___When_called()
        {
            // Arrange, Act
            var actual = ProjectInfo.Assembly.GetInterfaceTypes();

            // Assert
            actual.Should().Contain(typeof(IPublicInterface));
            actual.Should().Contain(typeof(IInternalInterface));
            actual.Should().NotContain(typeof(PublicEnum));
            actual.Should().NotContain(typeof(InternalEnum));
        }

        [Fact]
        public static void GetPublicInterfaceTypes___Should_throw_ArgumentNullException___When_parameter_type_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => AssemblyExtensions.GetPublicInterfaceTypes(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("assembly");
        }

        [Fact]
        public static void GetPublicInterfaceTypes___Should_return_public_interface_types___When_called()
        {
            // Arrange, Act
            var actual = ProjectInfo.Assembly.GetPublicInterfaceTypes();

            // Assert
            actual.Should().Contain(typeof(IPublicInterface));
            actual.Should().NotContain(typeof(IInternalInterface));
            actual.Should().NotContain(typeof(PublicEnum));
            actual.Should().NotContain(typeof(InternalEnum));
        }
    }
}

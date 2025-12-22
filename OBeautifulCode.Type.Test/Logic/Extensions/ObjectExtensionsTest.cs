// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FakeItEasy;
    using OBeautifulCode.Assertion.Recipes;
    using OBeautifulCode.AutoFakeItEasy;
    using Xunit;

    public static class ObjectExtensionsTest
    {
        [Fact]
        public static void FindAllInPropertyGraph___Should_throw_ArgumentNullException___When_parameter_rootObject_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ObjectExtensions.FindAllInPropertyGraph<IOperation>(null));

            // Assert
            actual.AsTest().Must().BeOfType<ArgumentNullException>();
            actual.Message.AsTest().Must().ContainString("rootObject");
        }

        [Fact]
        public static void FindAllInPropertyGraph___Should_return_rootObject___When_parameter_rootObject_is_of_type_TTarget()
        {
            // Arrange
            var rootObject = new TestClass
            {
                Details = A.Dummy<string>(),
            };

            // Act
            var actual = rootObject.FindAllInPropertyGraph<TestClass>();

            // Assert
            actual.AsTest().Must().HaveCount(1);
            actual.Single().Details.AsTest().Must().BeEqualTo(rootObject.Details);
        }

        [Fact]
        public static void FindAllInPropertyGraph___Should_return_rootObject_and_contained_object___When_parameter_rootObject_is_of_type_TTarget_and_contains_objects_of_type_TTarget()
        {
            // Arrange
            var details = Some.ReadOnlyDummies<string>(3).ToList();

            var rootObject = new TestClass
            {
                Details = details[0],
                ListOfObjects = new List<IObject>
                {
                    new TestClass
                    {
                        Details = details[1],
                    },
                },
                Object = new TestClass
                {
                    Details = details[2],
                },
            };

            // Act
            var actual = rootObject.FindAllInPropertyGraph<TestClass>();

            // Assert
            actual.AsTest().Must().HaveCount(3);
            actual.Select(_ => _.Details).Must().BeUnorderedEqualTo(details);
        }

        [Fact]
        public static void FindAllInPropertyGraph___Should_return_contained_objects___When_called()
        {
            // Arrange
            var details = Some.ReadOnlyDummies<string>(9).ToList();

            var rootObject = new TestClass
            {
                Details = details[0],
                ListOfObjects = new List<IObject>
                {
                    new TestClass
                    {
                        Details = details[1],
                        MapOfObjectToObject = new Dictionary<IObject, IObject[]>
                        {
                            {
                                new TestClass
                                {
                                    Details = details[2],
                                },
                                new IObject[]
                                {
                                    new NullReturningOp<string>(),
                                }
                            },
                        },
                    },
                    new TestClass
                    {
                        Details = details[3],
                        ArrayOfObjects = new IObject[]
                        {
                            new SimpleValue<TestClass>(
                                new TestClass
                                {
                                    Details = details[4],
                                }),
                            null,
                        },
                    },
                    new SimpleValue<TestClass>(
                        new TestClass
                        {
                            Details = details[5],
                        }),
                },
                ArrayOfObjects = new IObject[]
                {
                    null,
                    new SimpleValue<TestClass>(
                        new TestClass
                        {
                            Details = details[6],
                        }),
                },
                MapOfObjectToObject = new Dictionary<IObject, IObject[]>
                {
                    {
                        new SimpleValue<SimpleValue<TestClass>>(
                            new SimpleValue<TestClass>(
                                new TestClass
                                {
                                    Details = details[7],
                                })),
                        new IObject[]
                        {
                            null,
                        }
                    },
                },
                Object = new TestClass
                {
                    Details = details[8],
                },
            };

            // Act
            var actual = rootObject.FindAllInPropertyGraph<TestClass>();

            // Assert
            actual.AsTest().Must().HaveCount(9);
            actual.Select(_ => _.Details).Must().BeUnorderedEqualTo(details);
        }

        [Fact]
        public static void FindAllInPropertyGraph___Should_not_return_same_object_multiple_times___When_object_graph_has_same_target_multiple_times()
        {
            // Arrange
            var details = Some.ReadOnlyDummies<string>(7).ToList();

            var sharedObject = new TestClass
            {
                Details = details[6],
            };

            var rootObject = new TestClass
            {
                Details = details[0],
                ListOfObjects = new List<IObject>
                {
                    new TestClass
                    {
                        Details = details[1],
                        MapOfObjectToObject = new Dictionary<IObject, IObject[]>
                        {
                            {
                                sharedObject,
                                new IObject[]
                                {
                                    new NullReturningOp<string>(),
                                }
                            },
                        },
                    },
                    new TestClass
                    {
                        Details = details[2],
                        ArrayOfObjects = new IObject[]
                        {
                            new SimpleValue<TestClass>(
                                new TestClass
                                {
                                    Details = details[3],
                                }),
                            null,
                        },
                    },
                    new SimpleValue<TestClass>(
                        sharedObject),
                },
                ArrayOfObjects = new IObject[]
                {
                    null,
                    new SimpleValue<TestClass>(
                        new TestClass
                        {
                            Details = details[4],
                        }),
                },
                MapOfObjectToObject = new Dictionary<IObject, IObject[]>
                {
                    {
                        new SimpleValue<SimpleValue<TestClass>>(
                            new SimpleValue<TestClass>(
                                new TestClass
                                {
                                    Details = details[5],
                                })),
                        new IObject[]
                        {
                            null,
                        }
                    },
                },
                Object = sharedObject,
            };

            // Act
            var actual = rootObject.FindAllInPropertyGraph<TestClass>();

            // Assert
            actual.AsTest().Must().HaveCount(7);
            actual.Select(_ => _.Details).Must().BeUnorderedEqualTo(details);
        }

        private class TestClass : IObject
        {
            public string Details { get; set; }

            public List<IObject> ListOfObjects { get; set; }

            public IObject[] ArrayOfObjects { get; set; }

            public IReadOnlyDictionary<IObject, IObject[]> MapOfObjectToObject { get; set; }

            public IObject Object { get; set; }
        }
    }
}

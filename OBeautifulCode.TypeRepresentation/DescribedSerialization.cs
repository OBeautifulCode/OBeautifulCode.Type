// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DescribedSerialization.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.TypeRepresentation
{
    using System;

    using Spritely.Recipes;

    /// <summary>
    /// Represents a serialized object along with a description of the type of the object.
    /// </summary>
    public class DescribedSerialization
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DescribedSerialization"/> class.
        /// </summary>
        /// <param name="typeDescription">A description of the type of object serialized.</param>
        /// <param name="payload">The object serialized to a string.</param>
        /// <param name="serializer">The serializer used to generate the payload.</param>
        /// <exception cref="ArgumentNullException"><paramref name="typeDescription"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="payload"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="payload"/> is whitespace.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="serializer"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="serializer"/> is whitespace.</exception>
        public DescribedSerialization(TypeDescription typeDescription, string payload, string serializer)
        {
            new { typeDescription }.Must().NotBeNull().OrThrow();
            new { payload }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();
            new { serializer }.Must().NotBeNull().And().NotBeWhiteSpace().OrThrowFirstFailure();

            this.TypeDescription = typeDescription;
            this.Payload = payload;
            this.Serializer = serializer;
        }

        /// <summary>
        /// Gets a description of the type of object serialized.
        /// </summary>
        public TypeDescription TypeDescription { get; }

        /// <summary>
        /// Gets the object serialized to a string.
        /// </summary>
        public string Payload { get; }

        /// <summary>
        /// Gets the serializer used to generate the payload.
        /// </summary>
        public string Serializer { get; }
    }
}

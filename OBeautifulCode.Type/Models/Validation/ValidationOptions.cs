// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationOptions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// Various options that control how validation is performed.
    /// </summary>
    public partial class ValidationOptions : IModelViaCodeGen
    {
        /// <summary>
        /// Gets or sets the scope of the validation.
        /// </summary>
        public ValidationScope ValidationScope { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies when validation should stop.
        /// </summary>
        public ValidateUntil ValidateUntil { get; set; }

        /// <summary>
        /// Gets or sets the order in which self-validation and property validation occurs.
        /// </summary>
        public ValidationOrder ValidationOrder { get; set; }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IThrowOpExecutionDeemedNotApplicableExceptionOp.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// An operation that throws an <see cref="OpExecutionDeemedNotApplicableExceptionBase"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IThrowOpExecutionDeemedNotApplicableExceptionOp : IOperation, IHaveDetails
    {
    }
}

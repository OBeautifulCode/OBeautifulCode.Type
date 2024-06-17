// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperation.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    using System.Diagnostics.CodeAnalysis;

    using OBeautifulCode.CodeAnalysis.Recipes;

    /// <summary>
    /// An operation is, conceptually, a method on an interface.
    /// </summary>
    /// <remarks>
    /// Some operations return an object and some don't.  The operation needs to be named and should be suffixed with
    /// Op' (e.g. DoSomeWorkOp).  Operations have 0 or more parameters.  These define the data with which the operation
    /// is being performed and any behavioral variables.
    ///
    /// Only abstract classes should implement this interface.  Other interfaces should not implement this interface.
    /// By defining operations in classes it forces the consumer to specify one operation per class (i.e. you can't
    /// implement the same interface multiple times for different conceptual operations).  This simplifies the
    /// definition, consumption, and serialization of operations.
    ///
    /// <see cref="IVoidOperation"/> and <see cref="IReturningOperation{TResult}"/>
    /// differentiate operations that return versus those that don't.  When you implement one of these interfaces,
    /// the nature of the operation from a return perspective should be evident and
    /// declarative because it prevents the consumer from defining a void operation where a returning operation is
    /// actually required.  The TResult in <see cref="IReturningOperation{TResult}"/> is not used within the interface itself
    /// (e.g. used in any property or method defined within the interface).  However, the
    /// <see cref="IProtocol{TOperation}"/> associated with the operation requires this generic parameter to define
    /// the signature of the method that can Execute() the operation.
    /// </remarks>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = ObcSuppressBecause.CA1040_AvoidEmptyInterfaces_NeedToIdentifyGroupOfTypesAndPreferInterfaceOverAttribute)]
    public interface IOperation : IObject
    {
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelativeSortOrder.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Type
{
    /// <summary>
    /// The relative order of two objects being compared.
    /// </summary>
    public enum RelativeSortOrder
    {
        /// <summary>
        /// This instance precedes the other instance in the sort order.
        /// </summary>
        ThisInstancePrecedesTheOtherInstance = -1,

        /// <summary>
        /// This instance occurs in the same position in the sort order as the other instance.
        /// </summary>
        ThisInstanceOccursInTheSamePositionAsTheOtherInstance = 0,

        /// <summary>
        /// This instance follows the other instance in the sort order.
        /// </summary>
        ThisInstanceFollowsTheOtherInstance = 1,
    }
}

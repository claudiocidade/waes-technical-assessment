// <copyright file="IBuilder.cs" company="ClaudioCidade">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Domain.Builders
{
    /// <summary>
    /// An implementation contract for Builder classes.
    /// </summary>
    /// <typeparam name="T">Type of the object to be built.</typeparam>
    public interface IBuilder<T> where T : class
    {
        /// <summary>
        /// Runs the object build logic.
        /// </summary>
        /// <returns>An instance of the <see cref="T"/> class.</returns>
        T Build();
    }
}

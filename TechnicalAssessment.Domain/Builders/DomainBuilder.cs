// <copyright file="DomainBuilder.cs" company="ClaudioCidade">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Domain.Builders
{
    /// <summary>
    /// Domain objects builder implementation class.
    /// </summary>
    /// <typeparam name="T">Type of the object to be built.</typeparam>
    public abstract class DomainBuilder<T> : Builder<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainBuilder{T}"/> class.
        /// </summary>
        protected DomainBuilder()
        {
            this.IsUpdating = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainBuilder{T}"/> class.
        /// </summary>
        /// <param name="instance">An existing instance of the build type object.</param>
        protected DomainBuilder(T instance) : base(instance)
        {
            this.IsUpdating = true;
        }

        /// <summary>
        /// Gets a value indicating whether the builder is being used
        /// to apply updates to an existing instance of the built type.
        /// </summary>
        protected bool IsUpdating { get; private set; }
    }
}

// <copyright file="Builder.cs" company="ClaudioCidade">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Domain.Builders
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Base Builder pattern implementation class.
    /// </summary>
    /// <typeparam name="T">Type of the object to be built.</typeparam>
    public abstract class Builder<T> : IBuilder<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Builder{T}"/> class.
        /// </summary>
        protected Builder()
        {
            this.Result = this.CreateInstance();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder{T}"/> class.
        /// </summary>
        /// <param name="instance">An existing instance of the object that is being built
        /// so that the builder is used for modification only.</param>
        protected Builder(T instance)
        {
            this.Result = instance;
        }

        /// <summary>
        /// Gets an instance of the <see cref="T"/> class to be used
        /// during the build process.
        /// </summary>
        protected T Result { get; }

        /// <summary>
        /// Executes the object build process.
        /// </summary>
        /// <returns>An instance of the <see cref="T"/>.</returns>
        public T Build()
        {
            return this.Result;
        }

        /// <summary>
        /// Validates the object that is being built before actually 
        /// producing a <see cref="T"/> instance.
        /// </summary>
        /// <param name="instance">An instance of the building type.</param>
        protected virtual void ValidateBeforeBuilding(T instance)
        {
            // No default behavior specified
        }

        /// <summary>
        /// Executes an external builder and absorbs the results into the parent builder result.
        /// </summary>
        /// <typeparam name="TExternalObject">Type of the object built by the external builder.</typeparam>
        /// <param name="externalBuilder">An instance of the external builder to be invoked.</param>
        /// <returns>An instance of the object built by the external builder.</returns>
        protected TExternalObject InvokeExternalBuilder<TExternalObject>(IBuilder<TExternalObject> externalBuilder) where TExternalObject : class
        {
            return externalBuilder.Build();
        }

        /// <summary>
        /// Gets the internal constructor via Reflection and uses it 
        /// to create a new instance of the built type.
        /// </summary>
        /// <returns>A new instance of the built type.</returns>
        private T CreateInstance()
        {
            return typeof(T).GetTypeInfo().DeclaredConstructors.First().Invoke(null) as T;
        }
    }
}

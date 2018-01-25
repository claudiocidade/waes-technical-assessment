// <copyright file="Message.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Domain
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    public class Message
    {
        protected internal Message()
        {    
        }

        [BsonId]
        public Guid Id { get; protected internal set; }

        public string Data { get; protected internal set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><value>true.</value> if the specified object is equal to
        /// the current object; otherwise, <value>false.</value>.</returns>
        public override bool Equals(object obj)
        {
            return obj is Message && this.Data.Equals(((Message)obj).Data);
        }

        /// <summary>
        /// Provides a hash code for algorithms that need quick checks of object equality.
        /// </summary>
        /// <returns>A hash code for algorithms that need quick checks of object equality.</returns>
        public override int GetHashCode()
        {
            return this.Data.GetHashCode();
        }
    }
}

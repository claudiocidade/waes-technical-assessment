﻿// <copyright file="Message.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Domain
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Represents a binary carrier message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        protected internal Message()
        {    
        }

        /// <summary>
        /// Gets or sets the message identification.
        /// </summary>
        [BsonId]
        public Guid Id { get; protected internal set; }

        /// <summary>
        /// Gets or sets the base 64 encoded data for this message.
        /// </summary>
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

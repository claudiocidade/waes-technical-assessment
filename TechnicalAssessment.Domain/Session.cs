// <copyright file="Session.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Domain
{
    public class Session
    {
        protected internal Session()
        {
        }

        protected internal Session(string id)
        {
            Id = id;
        }

        public string Id { get; protected internal set; }

        public Message LeftSide { get; protected internal set; }

        public Message RightSide { get; protected internal set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><value>true.</value> if the specified object is equal to
        /// the current object; otherwise, <value>false.</value>.</returns>
        public override bool Equals(object obj)
        {
            return obj is Session && this.Id.Equals(((Session)obj).Id);
        }

        /// <summary>
        /// Provides a hash code for algorithms that need quick checks of object equality.
        /// </summary>
        /// <returns>A hash code for algorithms that need quick checks of object equality.</returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}

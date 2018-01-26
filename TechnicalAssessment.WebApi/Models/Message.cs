// <copyright file="Message.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.WebApi.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// A <see cref="Message"/> data contract representation model.
    /// </summary>
    [DataContract]
    public class Message
    {
        /// <summary>
        /// Gets or sets the Base 64 encoded binary data.
        /// </summary>
        [DataMember(Name = "data")]
        public string Data { get; set; }
    }
}

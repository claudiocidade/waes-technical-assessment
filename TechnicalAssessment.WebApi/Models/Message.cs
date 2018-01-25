// <copyright file="Message.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.WebApi.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Message
    {
        [DataMember(Name = "data")]
        public string Data { get; set; }
    }
}

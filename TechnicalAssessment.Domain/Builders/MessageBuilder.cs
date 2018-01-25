//  <copyright file="MessageBuilder.cs" company="WAES">
//   Copyright (c) All rights reserved.
//  </copyright>
namespace TechnicalAssessment.Domain.Builders
{
    /// <summary>
    /// A domain object builder class for <see cref="Domain.Message"/>.
    /// </summary>
    public class MessageBuilder : Builder<Message>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBuilder"/> class.
        /// </summary>
        /// <param name="message">An instance of an existing <see cref="Message"/> 
        /// that needs to be updated through this builder.</param>
        public MessageBuilder(Message message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBuilder"/> class.
        /// </summary>
        public MessageBuilder()
        {
        }

        /// <summary>
        /// Adds a <see cref="Message"/> to the <see cref="Message"/>.
        /// </summary>
        /// <param name="data">Base 64 enconded binary data.</param>
        /// <returns>This <see cref="MessageBuilder"/> instance.</returns>
        public MessageBuilder WithData(string data)
        {
            this.Result.Data = data;

            return this;
        }

        /// <summary>
        /// Validates the object that is being built before actually 
        /// producing a <see cref="Message"/> instance.
        /// </summary>
        /// <param name="instance">An instance of the building type.</param>
        protected override void ValidateBeforeBuilding(Message instance)
        {
        }
    }
}
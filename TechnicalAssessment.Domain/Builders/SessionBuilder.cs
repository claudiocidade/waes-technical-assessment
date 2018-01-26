//  <copyright file="SessionBuilder.cs" company="WAES">
//   Copyright (c) All rights reserved.
//  </copyright>
namespace TechnicalAssessment.Domain.Builders
{
    /// <summary>
    /// A domain object builder class for <see cref="Domain.Session"/>.
    /// </summary>
    public class SessionBuilder : Builder<Session>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionBuilder"/> class.
        /// </summary>
        /// <param name="session">An instance of an existing <see cref="Session"/> 
        /// that needs to be updated through this builder.</param>
        public SessionBuilder(Session session) : base(session)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionBuilder"/> class.
        /// </summary>
        /// <param name="sessionId"><see cref="Session"/> identification number.</param>
        public SessionBuilder(string sessionId)
        {
            this.Result.Id = sessionId;
        }

        /// <summary>
        /// Adds a <see cref="Session"/> to the <see cref="Session"/>.
        /// </summary>
        /// <param name="messageBuilder">An instance of the <see cref="Session"/> class.</param>
        /// <returns>This <see cref="SessionBuilder"/> instance.</returns>
        public SessionBuilder WithLeftSideMessage(IBuilder<Message> messageBuilder)
        {
            this.Result.LeftSide = this.InvokeExternalBuilder(messageBuilder);

            return this;
        }

        /// <summary>
        /// Adds a <see cref="Message"/> to the <see cref="Session"/>.
        /// </summary>
        /// <param name="messageBuilder">An instance of the <see cref="Session"/> class.</param>
        /// <returns>This <see cref="SessionBuilder"/> instance.</returns>
        public SessionBuilder WithRightSideMessage(IBuilder<Message> messageBuilder)
        {
            this.Result.RightSide = this.InvokeExternalBuilder(messageBuilder);

            return this;
        }

        /// <summary>
        /// Validates the object that is being built before actually 
        /// producing a <see cref="Session"/> instance.
        /// </summary>
        /// <param name="instance">An instance of the building type.</param>
        protected override void ValidateBeforeBuilding(Session instance)
        {
        }
    }
}
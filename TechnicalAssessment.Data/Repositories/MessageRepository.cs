// <copyright file="MessageRepository.cs" company="WAES">
//  Copyright (c) All rights reserved.
// </copyright>
namespace TechnicalAssessment.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using TechnicalAssessment.Data.Repositories.Contracts;
    using TechnicalAssessment.Domain;
    using TechnicalAssessment.Domain.Builders;

    /// <summary>
    /// A <see cref="Message"/> repository.
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        /// <summary>
        /// An instance of the MongoDB client that is going to be used for persistence.
        /// </summary>
        private readonly MongoClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepository"/> class.
        /// </summary>
        /// <param name="client">An instance of the <see cref="MongoClient"/> used by this repository.</param>
        public MessageRepository(MongoClient client)
        {
            this.client = client;
        }

        async Task<Message> IMessageRepository.Get(Guid id)
        {
            IMongoCollection<Message> collection = this.GetCollection();

            IAsyncCursor<Message> messages = await collection.FindAsync(s => s.Id == id);

            await messages.MoveNextAsync();

            if (!messages.Current.Any())
            {
                return null;
            }
            else
            {
                return messages.Current.First();
            }
        }

        async Task<Guid> IMessageRepository.Save(Message message)
        {
            IMongoCollection<Message> collection = this.GetCollection();

            await collection.InsertOneAsync(message);

            return message.Id;
        }

        private IMongoCollection<Message> GetCollection()
        {
            IMongoDatabase db = this.client.GetDatabase("waes");
            
            return db.GetCollection<Message>("message");
        }
    }
}
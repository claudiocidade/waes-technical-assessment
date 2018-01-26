// //  <copyright file="DatabaseClient.cs" company="WAES">
// //  Copyright (c) WAES. All rights reserved.
// //  </copyright>
namespace TechnicalAssessment.Data
{
    using MongoDB.Driver;

    /// <summary>
    /// Database client creator.
    /// </summary>
    public class DatabaseClient
    {
        /// <summary>
        /// Creates a new instance of a <see cref="MongoClient"/> using the provided connection string.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        /// <returns>An instance of the <see cref="MongoClient"/> class.</returns>
        public static MongoClient Create(string connectionString)
        {
            return new MongoClient(connectionString);
        } 
    }
}
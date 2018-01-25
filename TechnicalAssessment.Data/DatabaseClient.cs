// //  <copyright file="DatabaseClient.cs" company="Personow">
// //  Copyright (c) Personow. All rights reserved.
// //  </copyright>
namespace TechnicalAssessment.Data
{
    using MongoDB.Driver;

    public class DatabaseClient
    {
        public static MongoClient Create()
        {
            return new MongoClient("mongodb+srv://admin:admin@cluster0-5snvo.mongodb.net/test");
        } 
    }
}
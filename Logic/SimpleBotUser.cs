using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        public static string Reply(Message message)
        {
            var client = new MongoClient();

            var db = client.GetDatabase("Mongo");

            var col = db.GetCollection<BsonDocument>("tabela1");

            var doc = new BsonDocument
            {
                { "id", message.Id },
                { "texto", message.Text },
                { "app", "teste" }
            };


            col.InsertOne(doc);

            //var colFind = db.GetCollection<BsonDocument>("tabela1");

            col = null;

            db = null;

            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("Mongo");
            var col = db.GetCollection<BsonDocument>("tabela1");

            var filtro = Builders<BsonDocument>.Filter.Eq("id", id);
            //var filtro = Builders<BsonDocument>.Filter.Gt("id", 1);
            var res = col.Find(filtro).ToList();
            
            UserProfile p = new UserProfile();

            foreach (var item in res)
            {
                //p.Id = item.
                //p.Visitas = "";

            }



            



            return null;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
        }
    }
}
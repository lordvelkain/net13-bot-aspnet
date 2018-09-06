using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        static Dictionary<string, UserProfile> _dictProfiles = new Dictionary<string, UserProfile>();

        public static string Reply(Message message)
        {
            var id = message.Id;

            var profile = GetProfile(id);

            profile.Visitas += 1;

            SetProfile(id, profile);

            return $"{message.User} disse '{message.Text} e mandou {profile.Visitas} mensagens'";
        }

        public static void SalvarHistorico(Message message)
        {
            var client = new MongoClient("mongodb://localhost:27017");

            var doc = new BsonDocument
            {
                { "id", message.Id },
                { "texto", message.Text},
                { "app", "teste"}
            };

            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela01");
            col.InsertOne(doc);
        }

        public static UserProfile GetProfile(string id)
        {
            _dictProfiles.TryGetValue(id, out var profile);

            if( profile == null )
            {
                return new UserProfile()
                {
                    Id = id, Visitas = 0
                };
            }

            return profile;
        }

        public static void SetProfile(string id, UserProfile profile)
        {
            _dictProfiles[id] = profile;
        }
    }
}
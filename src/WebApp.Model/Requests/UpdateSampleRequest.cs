using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApp.Model.Requests
{
    public class UpdateSampleRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
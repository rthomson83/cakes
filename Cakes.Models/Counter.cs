using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cakes.Models
{
    public class Counter
    {
        [BsonId]
        public string Id { get; set; }
        public int Sequence { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Cakes.Models
{
    public class Cake
    {
        [BsonId]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required, MinLength(5), MaxLength(200)]
        public string Comment { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string YumFactor { get; set; }
    }
}
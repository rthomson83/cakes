using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Cakes.Models
{
    public class Cake
    {
        [BsonId]
        public int Id { get; set; }
        [Required(ErrorMessage = "The name can't be empty")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The comment can't be empty"), MinLength(5, ErrorMessage = "Comment must be at least 5 characters"), MaxLength(200, ErrorMessage = "Comment can't be longer that 200 characters")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "The image url can't be empty")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "The yum factor can't be empty"), Range(1, 5, ErrorMessage = "The yum factor must be between 1 and 5")]
        public int? YumFactor { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime PublishDate { get; set; }

        public int GenreId { get; set; }
        [JsonIgnore]
        public Genre Genre { get; set; }

        // 1 - 1
        [JsonIgnore]
        public int AuthorId { get; set; }
        //[JsonIgnore]
        public Author Author { get; set; }

    }
}
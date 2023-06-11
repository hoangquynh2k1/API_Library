using API_Library.Models;
using System.Collections.Generic;

namespace API_Library.Entities
{
    public class BookEntity
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int? PageNumber { get; set; }
        public int? Price { get; set; }
        public short? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public short? PositionId { get; set; }
        public Position Position { get; set; }
        public List<Image> Images { get; set; }
        public short? LanguageId { get; set; }
        public string Language { get; set; }
        public bool? Status { get; set; }
    }
}

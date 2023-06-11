using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Book
    {
        public Book()
        {
            Copies = new HashSet<Copy>();
            Images = new HashSet<Image>();
        }

        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int? PageNumber { get; set; }
        public int? Price { get; set; }
        public short? CategoryId { get; set; }
        public short? PositionId { get; set; }
        public short? LanguageId { get; set; }
        public bool? Status { get; set; }

        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Copy> Copies { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}

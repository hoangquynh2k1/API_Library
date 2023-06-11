using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public short CategoryId { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}

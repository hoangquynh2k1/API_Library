using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Language
    {
        public Language()
        {
            Books = new HashSet<Book>();
        }

        public short LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}

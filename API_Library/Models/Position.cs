using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Position
    {
        public Position()
        {
            Books = new HashSet<Book>();
        }

        public short PositionId { get; set; }
        public short? Shelf { get; set; }
        public short? Floor { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}

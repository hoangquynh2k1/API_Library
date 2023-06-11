using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Copy
    {
        public Copy()
        {
            BorrowingDetails = new HashSet<BorrowingDetail>();
        }

        public int CopyId { get; set; }
        public int? BookId { get; set; }
        public decimal? Durability { get; set; }
        public string Description { get; set; }
        public byte? BorrowStatus { get; set; }
        public bool? Status { get; set; }

        public virtual Book Book { get; set; }
        public virtual ICollection<BorrowingDetail> BorrowingDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class BorrowingDetail
    {
        public int BorrowingDetailId { get; set; }
        public int? BorrowingId { get; set; }
        public int? CopyId { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? Durability { get; set; }
        public string Description { get; set; }
        public byte? BorrowStatus { get; set; }
        public bool? Status { get; set; }

        public virtual Borrowing Borrowing { get; set; }
        public virtual Copy Copy { get; set; }
    }
}

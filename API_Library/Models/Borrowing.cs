using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Borrowing
    {
        public Borrowing()
        {
            BorrowingDetails = new HashSet<BorrowingDetail>();
        }

        public int BorrowingId { get; set; }
        public int? BorrowerId { get; set; }
        public short? StaffId { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public bool? Status { get; set; }
        public bool? NotificationStatus { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual staff Staff { get; set; }
        public virtual ICollection<BorrowingDetail> BorrowingDetails { get; set; }
    }
}

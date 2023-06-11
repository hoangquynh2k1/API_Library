using API_Library.Models;
using System;
using System.Collections.Generic;

namespace API_Library.Entities
{
    public class BorrowingEntity
    {
        public int BorrowingId { get; set; }
        public int? BorrowerId { get; set; }
        public short? StaffId { get; set; }
        public DateTime? BorrowedDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public bool? NotificationStatus { get; set; }
        public bool? BorrowStatus { get; set; }
        public int? Overdue { get; set; }
        public bool? Status { get; set; }
        public string Name { get; set; }
        public List<BorrowingDetail> Details { get; set; }

    }
}

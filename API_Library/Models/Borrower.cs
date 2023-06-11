using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Borrower
    {
        public Borrower()
        {
            Borrowings = new HashSet<Borrowing>();
        }

        public int BorrowerId { get; set; }
        public string Name { get; set; }
        public bool? Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? StartDay { get; set; }
        public int? AccountBalance { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; }
    }
}

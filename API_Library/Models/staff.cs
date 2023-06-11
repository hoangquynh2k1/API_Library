using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class staff
    {
        public staff()
        {
            Accounts = new HashSet<Account>();
            Borrowings = new HashSet<Borrowing>();
        }

        public short StaffId { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public bool? Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
        public DateTime? StartDay { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Borrowing> Borrowings { get; set; }
    }
}

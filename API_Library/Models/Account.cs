using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Account
    {
        public Account()
        {
            Logins = new HashSet<Login>();
        }

        public int AccountId { get; set; }
        public short? StaffId { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }

        public virtual staff Staff { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
    }
}

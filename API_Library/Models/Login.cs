using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Login
    {
        public int LoginId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? LoginDate { get; set; }
        public bool? Status { get; set; }

        public virtual Account Account { get; set; }
    }
}

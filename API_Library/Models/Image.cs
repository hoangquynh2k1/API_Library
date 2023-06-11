using System;
using System.Collections.Generic;

#nullable disable

namespace API_Library.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int? BookId { get; set; }
        public string Path { get; set; }
        public bool? Status { get; set; }

        public virtual Book Book { get; set; }
    }
}

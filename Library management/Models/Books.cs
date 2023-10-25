using System;
using System.Collections.Generic;

namespace Library_management.Models
{
    public partial class Books
    {
        public int BookId { get; set; }

        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public int Owner { get; set; }
        public int? LendedBy { get; set; }

        public DateTime? ReturnDate { get; set; }
        public int? ReservedBy { get; set; }

        public virtual Category Category { get; set; }
        public virtual Users LendedByNavigation { get; set; }
        public virtual Users OwnerNavigation { get; set; }
        public virtual Users ReservedByNavigation { get; set; }
    }
}

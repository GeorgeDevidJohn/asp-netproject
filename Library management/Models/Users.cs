using System;
using System.Collections.Generic;

namespace Library_management.Models
{
    public partial class Users
    {
        public Users()
        {
            BooksLendedByNavigation = new HashSet<Books>();
            BooksOwnerNavigation = new HashSet<Books>();
            BooksReservedByNavigation = new HashSet<Books>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Admin { get; set; }

        public virtual ICollection<Books> BooksLendedByNavigation { get; set; }
        public virtual ICollection<Books> BooksOwnerNavigation { get; set; }
        public virtual ICollection<Books> BooksReservedByNavigation { get; set; }
    }
}

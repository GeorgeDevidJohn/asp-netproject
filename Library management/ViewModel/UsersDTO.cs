using Library_management.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_management.ViewModel
{
    public class UsersDTO
    {
        public UsersDTO()
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

using System;
using System.Collections.Generic;

namespace Library_management.Models
{
    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Books>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}

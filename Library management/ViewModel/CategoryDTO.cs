using Library_management.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_management.ViewModel
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {
            Books = new HashSet<Books>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}

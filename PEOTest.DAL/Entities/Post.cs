using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompEmp> CompEmps { get; set; }

        public Post()
        {
            CompEmps = new List<CompEmp>();
        }
    }
}

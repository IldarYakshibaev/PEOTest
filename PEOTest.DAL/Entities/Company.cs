using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.DAL.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompEmp> CompEmps { get; set; }

        public Company()
        {
            CompEmps = new List<CompEmp>();
        }
    }
}

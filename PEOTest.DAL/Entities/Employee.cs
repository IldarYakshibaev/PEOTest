using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CompEmp> CompEmps { get; set; }

        public Employee()
        {
            CompEmps = new List<CompEmp>();
        }
    }
}

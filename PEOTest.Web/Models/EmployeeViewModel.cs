using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEOTest.Web.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CompEmpViewModel> CompEmps { get; set; }
    }
}

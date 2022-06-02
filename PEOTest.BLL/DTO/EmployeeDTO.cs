using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CompEmpDTO> CompEmpDTOs { get; set; }

        public EmployeeDTO()
        {
            CompEmpDTOs = new List<CompEmpDTO>();
        }
    }
}

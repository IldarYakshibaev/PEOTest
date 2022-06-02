using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompEmpDTO> CompEmpDTOs { get; set; }

        public CompanyDTO()
        {
            CompEmpDTOs = new List<CompEmpDTO>();
        }
    }
}

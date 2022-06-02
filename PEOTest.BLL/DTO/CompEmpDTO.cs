using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.DTO
{
    public class CompEmpDTO
    {
        public EmployeeDTO EmployeeDTO { get; set; }
        public CompanyDTO CompanyDTO { get; set; }
        public PostDTO PostDTO { get; set; }
        public SubdivisionDTO SubdivisionDTO { get; set; }
    }
}

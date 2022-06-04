using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.DTO
{
    public class CompEmpDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int PostId { get; set; }
        public int SubdivisionId { get; set; }
        public EmployeeDTO Employee { get; set; }
        public CompanyDTO Company { get; set; }
        public PostDTO Post { get; set; }
        public SubdivisionDTO Subdivision { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEOTest.Web.Models
{
    public class CompEmpViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int PostId { get; set; }
        public int SubdivisionId { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public CompanyViewModel Company { get; set; }
        public PostViewModel Post { get; set; }
        public SubdivisionViewModel Subdivision { get; set; }
    }
}

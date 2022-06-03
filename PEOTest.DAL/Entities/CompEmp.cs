using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.DAL.Entities
{
    public class CompEmp
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int CompanyId { get; set; }
        public int PostId { get; set; }
        public int SubdivisionId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Company Company { get; set; }
        public virtual Post Post { get; set; }
        public virtual Subdivision Subdivision { get; set; }
    }
}

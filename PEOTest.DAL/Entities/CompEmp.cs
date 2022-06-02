using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.DAL.Entities
{
    public class CompEmp
    {
        public Employee Employee { get; set; }
        public Company Company { get; set; }
        public Post Post { get; set; }
        public Subdivision Subdivision { get; set; }
    }
}

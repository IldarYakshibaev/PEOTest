using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEOTest.Web.Models
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<CompEmpViewModel> CompEmps { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEOTest.Web.Models
{
    public class SubdivisionViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано Подразделение")]
        public string Name { get; set; }

        public virtual ICollection<CompEmpViewModel> CompEmps { get; set; }
    }
}

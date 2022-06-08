using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEOTest.Web.Models
{
    public class EmpIndexViewModel
    {
        public IEnumerable<CompEmpViewModel> CompEmpList {get;set;}
        public string SortName { get; set; }
        public string SortOrder { get; set; }

        public int CompanyId { get; set; }
        public List<SelectListItem> Company { get; set; }
        public int SubdivisionId { get; set; }
        public List<SelectListItem> Subdivision { get; set; }
        public int PostId { get; set; }
        public List<SelectListItem> Post { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

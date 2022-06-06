using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PEOTest.Web.Models
{
    public class EmployeeModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public SelectList Company { get; set; }
        public int SubdivisionId { get; set; }
        public string SubdivisionName { get; set; }
        public SelectList Subdivision { get; set; }
        public int PostId { get; set; }
        [Required]
        public string PostName { get; set; }
        public List<SelectListItem> Post { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

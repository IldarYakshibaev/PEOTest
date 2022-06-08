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
        public int CompEmpId { get; set; }
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "Не указана Компания")]
        public string CompanyName { get; set; }
        public List<SelectListItem> Company { get; set; }
        public int SubdivisionId { get; set; }
        [Required(ErrorMessage = "Не указано Подразделение")]
        public string SubdivisionName { get; set; }
        public List<SelectListItem> Subdivision { get; set; }
        public int PostId { get; set; }
        [Required (ErrorMessage = "Не указана Должность")]
        public string PostName { get; set; }
        public List<SelectListItem> Post { get; set; }
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Не указана Фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Не указано Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указано Отчество")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Не указан Телефон")]
        [RegularExpression(@"((\d{1,2})|(\+\d{1,2}))?((\(\d{3}\))|(\-?\d{3}\-)|(\d{3}))((\d{3}\-\d{4})|(\d{3}\-\d\d\-\d\d)|(\d{7})|(\d{3}\-\d\-\d{3}))", ErrorMessage = "Некорректный номер телефона.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Не указана Почта")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
    }
}

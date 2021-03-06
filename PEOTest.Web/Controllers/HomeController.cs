using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PEOTest.BLL.DTO;
using PEOTest.BLL.Infrastructure;
using PEOTest.BLL.Interfaces;
using PEOTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PEOTest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICompEmpService _compEmpService;
        private ICompanyService _companyService;
        private IEmployeeService _employeeService;
        private IPostService _postService;
        private ISubdivisionService _subdivisionService;

        public HomeController(ILogger<HomeController> logger, 
            ICompEmpService compEmpService,
            ICompanyService companyService,
            IEmployeeService employeeService,
            IPostService postService,
            ISubdivisionService subdivisionService)
        {
            _logger = logger;
            _compEmpService = compEmpService;
            _companyService = companyService;
            _employeeService = employeeService;
            _postService = postService;
            _subdivisionService = subdivisionService;
        }

        public IActionResult Index(string sortName = "Employee.Surname", string sortOrder = "Ascending",
            int companyId = 0, int subdivisionId = 0, int postId = 0, string surname = "", string name = "", 
            string patronymic = "", string phone = "", string email = "")
        {
            IEnumerable<CompEmpDTO> compEmpDTO = _compEmpService
                .GetAll(companyId, subdivisionId, postId, 
                surname, name, patronymic, 
                phone, email,
                sortName + " " + sortOrder);

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<CompanyDTO, CompanyViewModel>();
                cfg.CreateMap<PostDTO, PostViewModel>();
                cfg.CreateMap<SubdivisionDTO, SubdivisionViewModel>();
            })
                .CreateMapper();

            EmpIndexViewModel model = new EmpIndexViewModel();
            model.CompEmpList = mapper.Map<IEnumerable<CompEmpDTO>, List<CompEmpViewModel>>(compEmpDTO);
            model.SortName = sortName;
            model.SortOrder = sortOrder == "Ascending" ? "Descending" : "Ascending";
            model.CompanyId = companyId;
            model.Company = _companyService.GetAllSL(companyId).ToList();
            model.SubdivisionId = subdivisionId;
            model.Subdivision = _subdivisionService.GetAllSL(subdivisionId).ToList();
            model.PostId = postId;
            model.Post = _postService.GetAllSL(postId).ToList();
            model.Surname = surname;
            model.Name = name;
            model.Patronymic = patronymic;
            model.Phone = phone;
            model.Email = email;

            return View(model);
        }
        public IActionResult Create()
        {
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<CompanyDTO, CompanyViewModel>();
                cfg.CreateMap<PostDTO, PostViewModel>();
                cfg.CreateMap<SubdivisionDTO, SubdivisionViewModel>();
            })
                .CreateMapper();

            EmployeeModel model = new EmployeeModel();
            model.Company = _companyService
                .GetAllSL().ToList();
            model.Subdivision = _subdivisionService
                .GetAllSL().ToList();
            model.Post = _postService
                .GetAllSL().ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(EmployeeModel model)
        {
            //db.Users.Add(user);
            //await db.SaveChangesAsync();
            try
            {
                CompanyDTO companyDTO = new CompanyDTO()
                {
                    Id = model.CompanyId,
                    Name = model.CompanyName
                };
                model.CompanyId = _companyService
                    .Create(companyDTO);
                companyDTO.Id = model.CompanyId;

                SubdivisionDTO subdivisionDTO = new SubdivisionDTO()
                {
                    Id = model.SubdivisionId,
                    Name = model.SubdivisionName
                };
                model.SubdivisionId = _subdivisionService
                    .Create(subdivisionDTO);
                subdivisionDTO.Id = model.SubdivisionId;

                PostDTO postDTO = new PostDTO()
                {
                    Id = model.PostId,
                    Name = model.PostName
                };
                model.PostId = _postService
                    .Create(postDTO);
                postDTO.Id = model.PostId;

                EmployeeDTO employeeDTO = new EmployeeDTO()
                {
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Phone = model.Phone,
                    Email = model.Email
                };
                employeeDTO.Id = _employeeService
                    .Create(employeeDTO);

                _compEmpService.Create(companyDTO,
                    subdivisionDTO,
                    postDTO,
                    employeeDTO);

                return RedirectToAction("Index");
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            model.Company = _companyService
                .GetAllSL(model.CompanyId)
                .ToList();

            model.Subdivision = _subdivisionService
                .GetAllSL(model.SubdivisionId)
                .ToList();

            model.Post = _postService
                .GetAllSL(model.PostId)
                .ToList();

            return View(model);
        }

        public IActionResult Edit(int empId)
        {

            CompEmpDTO compEmpDTO = _compEmpService
                .GetById(empId);

            EmployeeModel model = new EmployeeModel()
            {
                CompEmpId = compEmpDTO.Id,
                CompanyId = compEmpDTO.CompanyId,
                SubdivisionId = compEmpDTO.SubdivisionId,
                PostId = compEmpDTO.PostId,
                EmployeeId = compEmpDTO.EmployeeId,
                Surname = compEmpDTO.Employee.Surname,
                Name = compEmpDTO.Employee.Name,
                Patronymic = compEmpDTO.Employee.Patronymic,
                Phone = compEmpDTO.Employee.Phone,
                Email = compEmpDTO.Employee.Email
            };

            model.Company = _companyService
                .GetAllSL(model.CompanyId)
                .ToList();
            model.Subdivision = _subdivisionService
                .GetAllSL(model.SubdivisionId)
                .ToList();
            model.Post = _postService
                .GetAllSL(model.PostId)
                .ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeModel model)
        {
            try
            {
                CompanyDTO companyDTO = new CompanyDTO()
                {
                    Id = model.CompanyId,
                    Name = model.CompanyName
                };
                model.CompanyId = _companyService
                    .Create(companyDTO);
                companyDTO.Id = model.CompanyId;

                SubdivisionDTO subdivisionDTO = new SubdivisionDTO()
                {
                    Id = model.SubdivisionId,
                    Name = model.SubdivisionName
                };
                model.SubdivisionId = _subdivisionService
                    .Create(subdivisionDTO);
                subdivisionDTO.Id = model.SubdivisionId;

                PostDTO postDTO = new PostDTO()
                {
                    Id = model.PostId,
                    Name = model.PostName
                };
                model.PostId = _postService
                    .Create(postDTO);
                postDTO.Id = model.PostId;

                EmployeeDTO employeeDTO = new EmployeeDTO()
                {
                    Id = model.EmployeeId,
                    Surname = model.Surname,
                    Name = model.Name,
                    Patronymic = model.Patronymic,
                    Phone = model.Phone,
                    Email = model.Email
                };
                employeeDTO.Id = _employeeService
                    .Edit(employeeDTO);

                _compEmpService.Edit(model.CompEmpId,
                    companyDTO,
                    subdivisionDTO,
                    postDTO,
                    employeeDTO);

                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            model.Company = _companyService
                .GetAllSL(model.CompanyId)
                .ToList();

            model.Subdivision = _subdivisionService
                .GetAllSL(model.SubdivisionId)
                .ToList();

            model.Post = _postService
                .GetAllSL(model.PostId)
                .ToList();

            return View(model);
        }

        public IActionResult Delete(int empId)
        {
            _compEmpService.Delete(empId);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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

        public IActionResult Index()
        {
            IEnumerable<CompEmpDTO> compEmpDTO = _compEmpService
                .GetAllCompEmp();
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<CompanyDTO, CompanyViewModel>();
                cfg.CreateMap<PostDTO, PostViewModel>();
                cfg.CreateMap<SubdivisionDTO, SubdivisionViewModel>();
            })
                .CreateMapper();
            List<CompEmpViewModel> model = mapper.Map<IEnumerable<CompEmpDTO>, List<CompEmpViewModel>>(compEmpDTO);

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
                .GetAllCompanySL().ToList();
            model.Subdivision = _subdivisionService
                .GetAllSubdivisionSL().ToList();
            model.Post = _postService
                .GetAllPostSL().ToList();

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
                    .CreateCompany(companyDTO);
                companyDTO.Id = model.CompanyId;

                SubdivisionDTO subdivisionDTO = new SubdivisionDTO()
                {
                    Id = model.SubdivisionId,
                    Name = model.SubdivisionName
                };
                model.SubdivisionId = _subdivisionService
                    .CreateSubdivision(subdivisionDTO);
                subdivisionDTO.Id = model.SubdivisionId;

                PostDTO postDTO = new PostDTO()
                {
                    Id = model.PostId,
                    Name = model.PostName
                };
                model.PostId = _postService
                    .CreatePost(postDTO);
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
                    .CreateEmployee(employeeDTO);

                _compEmpService.CreateComEmp(companyDTO,
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
                .GetAllCompanySL(model.CompanyId)
                .ToList();

            model.Subdivision = _subdivisionService
                .GetAllSubdivisionSL(model.SubdivisionId)
                .ToList();

            model.Post = _postService
                .GetAllPostSL(model.PostId)
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
                .GetAllCompanySL(model.CompanyId)
                .ToList();
            model.Subdivision = _subdivisionService
                .GetAllSubdivisionSL(model.SubdivisionId)
                .ToList();
            model.Post = _postService
                .GetAllPostSL(model.PostId)
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
                    .CreateCompany(companyDTO);
                companyDTO.Id = model.CompanyId;

                SubdivisionDTO subdivisionDTO = new SubdivisionDTO()
                {
                    Id = model.SubdivisionId,
                    Name = model.SubdivisionName
                };
                model.SubdivisionId = _subdivisionService
                    .CreateSubdivision(subdivisionDTO);
                subdivisionDTO.Id = model.SubdivisionId;

                PostDTO postDTO = new PostDTO()
                {
                    Id = model.PostId,
                    Name = model.PostName
                };
                model.PostId = _postService
                    .CreatePost(postDTO);
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
                    .EditEmployee(employeeDTO);

                _compEmpService.EditComEmp(model.CompEmpId,
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
                .GetAllCompanySL(model.CompanyId)
                .ToList();

            model.Subdivision = _subdivisionService
                .GetAllSubdivisionSL(model.SubdivisionId)
                .ToList();

            model.Post = _postService
                .GetAllPostSL(model.PostId)
                .ToList();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
                .GetAllCompanySL();
            model.Post = _postService
                .GetAllPostSL();
            /*IEnumerable<PostDTO> postDTO = _postService.GetAllPost();
            model.Post = mapper
                .Map<IEnumerable<PostDTO>, List<PostViewModel>>(postDTO);*/

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(EmployeeModel model)
        {
            //db.Users.Add(user);
            //await db.SaveChangesAsync();
            try
            {
                PostDTO postDTO = new PostDTO()
                {
                    Id = model.PostId,
                    Name = model.PostName
                };
                postDTO.Id = _postService.CreatePost(postDTO);
                return RedirectToAction("Index");
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            model.Post = _postService
                .GetAllPostSL();
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

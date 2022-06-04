using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PEOTest.BLL.DTO;
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

        public HomeController(ILogger<HomeController> logger, ICompEmpService compEmpService)
        {
            _logger = logger;
            _compEmpService = compEmpService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CompEmpDTO> compEmpDTO = _compEmpService.GetCompEmps();
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<CompanyDTO, CompanyViewModel>();
                cfg.CreateMap<PostDTO, PostViewModel>();
                cfg.CreateMap<SubdivisionDTO, SubdivisionViewModel>();
            }).CreateMapper();
            IQueryable<CompEmpViewModel> model = mapper.Map<IEnumerable<CompEmpDTO>, IQueryable<CompEmpViewModel>>(compEmpDTO);

            return View(await model.ToListAsync());
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

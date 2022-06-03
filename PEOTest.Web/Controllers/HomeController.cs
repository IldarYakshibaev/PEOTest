using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            IEnumerable<CompEmpDTO> compEmpDTO = _compEmpService.GetCompEmps();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CompEmpDTO, CompEmpViewModel>()).CreateMapper();
            List<CompEmpViewModel> model = mapper.Map<IEnumerable<CompEmpDTO>, List<CompEmpViewModel>>(compEmpDTO);

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

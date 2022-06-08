using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PEOTest.BLL.DTO;
using PEOTest.BLL.Infrastructure;
using PEOTest.BLL.Interfaces;
using PEOTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEOTest.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;
        private ICompanyService _companyService;

        public CompanyController(ILogger<CompanyController> logger,
            ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }
        public ActionResult Index()
        {
            IEnumerable<CompanyDTO> companyDTO = _companyService
                .GetAll();

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<CompanyDTO, CompanyViewModel>();
                cfg.CreateMap<PostDTO, PostViewModel>();
                cfg.CreateMap<SubdivisionDTO, SubdivisionViewModel>();
            })
                .CreateMapper();
            IEnumerable<CompanyViewModel> model = mapper.Map<IEnumerable<CompanyDTO>, List<CompanyViewModel>>(companyDTO);

            return View(model);
        }

        public ActionResult Edit(int companyId)
        {
            CompanyDTO companyDTO = _companyService
                .GetById(companyId);

            CompanyViewModel model = new CompanyViewModel()
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CompanyViewModel model)
        {
            try
            {
                CompanyDTO companyDTO = new CompanyDTO()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                model.Id = _companyService
                    .Edit(companyDTO);

                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(model);
        }
    }
}

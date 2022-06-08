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
    public class SubdivisionController : Controller
    {
        private readonly ILogger<SubdivisionController> _logger;
        private ISubdivisionService _subdivisionService;

        public SubdivisionController(ILogger<SubdivisionController> logger,
            ISubdivisionService subdivisionService)
        {
            _logger = logger;
            _subdivisionService = subdivisionService;
        }
        public ActionResult Index()
        {
            IEnumerable<SubdivisionDTO> subdivisionDTO = _subdivisionService
                .GetAllSubdivision();

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<CompanyDTO, CompanyViewModel>();
                cfg.CreateMap<PostDTO, PostViewModel>();
                cfg.CreateMap<SubdivisionDTO, SubdivisionViewModel>();
            })
                .CreateMapper();
            IEnumerable<SubdivisionViewModel> model = mapper.Map<IEnumerable<SubdivisionDTO>, List<SubdivisionViewModel>>(subdivisionDTO);

            return View(model);
        }

        public ActionResult Edit(int subdivisionId)
        {
            SubdivisionDTO subdivisionDTO = _subdivisionService
                .GetById(subdivisionId);

            SubdivisionViewModel model = new SubdivisionViewModel()
            {
                Id = subdivisionDTO.Id,
                Name = subdivisionDTO.Name
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SubdivisionViewModel model)
        {
            try
            {
                SubdivisionDTO subdivisionDTO = new SubdivisionDTO()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                model.Id = _subdivisionService
                    .Edit(subdivisionDTO);

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

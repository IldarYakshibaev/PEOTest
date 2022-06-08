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
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private IPostService _postService;

        public PostController(ILogger<PostController> logger,
            IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }
        public ActionResult Index()
        {
            IEnumerable<PostDTO> postDTO = _postService
                .GetAllPost();

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmpDTO, CompEmpViewModel>();
                cfg.CreateMap<EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<CompanyDTO, CompanyViewModel>();
                cfg.CreateMap<PostDTO, PostViewModel>();
                cfg.CreateMap<SubdivisionDTO, SubdivisionViewModel>();
            })
                .CreateMapper();
            IEnumerable<PostViewModel> model = mapper.Map<IEnumerable<PostDTO>, List<PostViewModel>>(postDTO);

            return View(model);
        }

        public ActionResult Edit(int postId)
        {
            PostDTO postDTO = _postService
                .GetById(postId);

            PostViewModel model = new PostViewModel()
            {
                Id = postDTO.Id,
                Name = postDTO.Name
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel model)
        {
            try
            {
                PostDTO postDTO = new PostDTO()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                model.Id = _postService
                    .Edit(postDTO);

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

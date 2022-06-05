﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEOTest.BLL.DTO;
using PEOTest.BLL.Infrastructure;
using PEOTest.BLL.Interfaces;
using PEOTest.DAL;
using PEOTest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEOTest.BLL.Services
{
    public class PostService : IPostService
    {
        public EFDbContext _context;

        public PostService(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<PostDTO> GetAllPost()
        {
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmp, CompEmpDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Company, CompanyDTO>();
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Subdivision, SubdivisionDTO>();
            })
                .CreateMapper();

            return mapper
                .Map<IEnumerable<Post>, List<PostDTO>>(_context.Post.ToList());
        }
        public List<SelectListItem> GetAllPostSL()
        {
            return new List<SelectListItem>(GetAllPost()
                .Select(a => new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }));
        }
        public int CreatePost(PostDTO postDTO)
        {

            return 1;
            throw new ValidationException("Ошибка", "");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
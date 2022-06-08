using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public PostDTO GetById(int id)
        {
            if (id == 0)
            {
                throw new ValidationException("Данная должность не существует", "");
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmp, CompEmpDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Company, CompanyDTO>();
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Subdivision, SubdivisionDTO>();
            })
                .CreateMapper();
            return mapper.Map<Post, PostDTO>(_context.Post.FirstOrDefault(a => a.Id == id));
        }
        public IEnumerable<SelectListItem> GetAllPostSL(int postId = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { 
                Text = "", 
                Value = "0", 
                Selected = postId == 0 ? true : false
            });

            list.AddRange(GetAllPost()
                .Select(a => new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.Id.ToString(),
                    Selected = postId == a.Id ? true : false
                }));

            return list;
        }
        public int CreatePost(PostDTO postDTO)
        {
            if(postDTO.Id == 0 && (postDTO.Name == "" || postDTO.Name == null))
            {
                throw new ValidationException("Не указана Должность", "PostName");
            }
            if (postDTO.Id != 0)
            {
                return postDTO.Id;
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<PostDTO, Post>();
            })
                .CreateMapper();

            Post post = mapper
                .Map<PostDTO, Post>(postDTO);

            _context.Post.Add(post);
            _context.SaveChanges();

            return post.Id;

            //throw new ValidationException("Ошибка", "");
        }
        public int Edit(PostDTO postDTO)
        {
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<PostDTO, Post>();
            })
                .CreateMapper();

            Post post = mapper
                .Map<PostDTO, Post>(postDTO);

            _context.Entry(post).State = EntityState.Modified;
            _context.SaveChanges();

            return post.Id;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

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
    public class SubdivisionService : ISubdivisionService
    {
        public EFDbContext _context;

        public SubdivisionService(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SubdivisionDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmp, CompEmpDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Company, CompanyDTO>();
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Subdivision, SubdivisionDTO>();
            })
                .CreateMapper();
            return mapper.Map<IEnumerable<Subdivision>, List<SubdivisionDTO>>(_context.Subdivision.ToList());
        }
        public SubdivisionDTO GetById(int id)
        {
            if (id == 0)
            {
                throw new ValidationException("Данное подразделение не существует", "");
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmp, CompEmpDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Company, CompanyDTO>();
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Subdivision, SubdivisionDTO>();
            })
                .CreateMapper();
            return mapper.Map<Subdivision, SubdivisionDTO>(_context.Subdivision.FirstOrDefault(a => a.Id == id));
        }
        public IEnumerable<SelectListItem> GetAllSL(int id = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Text = "",
                Value = "0",
                Selected = id == 0 ? true : false
            });

            list.AddRange(GetAll()
                .Select(a => new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.Id.ToString(),
                    Selected = id == a.Id ? true : false
                }));

            return list;
        }
        public int Create(SubdivisionDTO subdivisionDTO)
        {
            if (subdivisionDTO.Id == 0 && (subdivisionDTO.Name == "" || subdivisionDTO.Name == null))
            {
                throw new ValidationException("Не указано Подразделение", "SubdivisionName");
            }
            if (subdivisionDTO.Id != 0)
            {
                return subdivisionDTO.Id;
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<SubdivisionDTO, Subdivision>();
            })
                .CreateMapper();

            Subdivision subdivision = mapper
                .Map<SubdivisionDTO, Subdivision>(subdivisionDTO);

            _context.Subdivision.Add(subdivision);
            _context.SaveChanges();

            return subdivision.Id;
        }
        public int Edit(SubdivisionDTO subdivisionDTO)
        {
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<SubdivisionDTO, Subdivision>();
            })
                .CreateMapper();

            Subdivision subdivision = mapper
                .Map<SubdivisionDTO, Subdivision>(subdivisionDTO);

            _context.Entry(subdivision).State = EntityState.Modified;
            _context.SaveChanges();

            return subdivision.Id;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

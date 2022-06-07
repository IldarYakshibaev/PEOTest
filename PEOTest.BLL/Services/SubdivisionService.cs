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
    public class SubdivisionService : ISubdivisionService
    {
        public EFDbContext _context;

        public SubdivisionService(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SubdivisionDTO> GetAllSubdivision()
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
        public IEnumerable<SelectListItem> GetAllSubdivisionSL(int subdivisionId = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Text = "",
                Value = "0",
                Selected = subdivisionId == 0 ? true : false
            });

            list.AddRange(GetAllSubdivision()
                .Select(a => new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.Id.ToString(),
                    Selected = subdivisionId == a.Id ? true : false
                }));

            return list;
        }
        public int CreateSubdivision(SubdivisionDTO subdivisionDTO)
        {
            if (subdivisionDTO.Id == 0 && (subdivisionDTO.Name == "" || subdivisionDTO.Name == null))
            {
                throw new ValidationException("Не указано Подразделение", "SubdivisionName");
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompanyDTO, Company>();
            })
                .CreateMapper();

            Subdivision subdivision = mapper
                .Map<SubdivisionDTO, Subdivision>(subdivisionDTO);

            _context.Subdivision.Add(subdivision);
            _context.SaveChanges();

            return subdivision.Id;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

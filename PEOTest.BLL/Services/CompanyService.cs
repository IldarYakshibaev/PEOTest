using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using PEOTest.BLL.DTO;
using PEOTest.BLL.Interfaces;
using PEOTest.DAL;
using PEOTest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PEOTest.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        public EFDbContext _context;

        public CompanyService(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CompanyDTO> GetAllCompany()
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
                .Map<IEnumerable<Company>, List<CompanyDTO>>(_context.Company.ToList());
        }
        public SelectList GetAllCompanySL()
        {
            return new SelectList(GetAllCompany()
                .Select(a => new SelectListItem() { 
                    Text = a.Name, Value = a.Id .ToString()
                }));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

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
        public CompanyDTO GetById(int id)
        {
            if (id == 0)
            {
                throw new ValidationException("Данная компания не существует", "");
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmp, CompEmpDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Company, CompanyDTO>();
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Subdivision, SubdivisionDTO>();
            })
                .CreateMapper();
            return mapper.Map<Company, CompanyDTO>(_context.Company.FirstOrDefault(a => a.Id == id));
        }
        public IEnumerable<SelectListItem> GetAllCompanySL(int companyId = 0)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Text = "",
                Value = "0",
                Selected = companyId == 0 ? true : false
            });

            list.AddRange(GetAllCompany()
                .Select(a => new SelectListItem()
                {
                    Text = a.Name,
                    Value = a.Id.ToString(),
                    Selected = companyId == a.Id ? true : false
                }));

            return list;
        }
        public int CreateCompany(CompanyDTO companyDTO)
        {
            if (companyDTO.Id == 0 && (companyDTO.Name == "" || companyDTO.Name == null))
            {
                throw new ValidationException("Не указана Компания", "CompanyName");
            }
            if(companyDTO.Id != 0)
            {
                return companyDTO.Id;
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompanyDTO, Company>();
            })
                .CreateMapper();

            Company company = mapper
                .Map<CompanyDTO, Company>(companyDTO);

            _context.Company.Add(company);
            _context.SaveChanges();

            return company.Id;
        }
        public int Edit(CompanyDTO companyDTO)
        {
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompanyDTO, Company>();
            })
                .CreateMapper();

            Company company = mapper
                .Map<CompanyDTO, Company>(companyDTO);

            _context.Entry(company).State = EntityState.Modified;
            _context.SaveChanges();

            return company.Id;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

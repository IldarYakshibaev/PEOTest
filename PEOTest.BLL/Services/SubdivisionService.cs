using AutoMapper;
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

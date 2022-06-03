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
    public class CompEmpService : ICompEmpService
    {
        public EFDbContext _context;

        public CompEmpService(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CompEmpDTO> GetCompEmps()
        {
            if (!_context.CompEmp.Any())
            {
                SampleDate();
            }



            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CompEmp, CompEmpDTO>())
                    .CreateMapper();
            return mapper.Map<IEnumerable<CompEmp>, List<CompEmpDTO>>(_context.CompEmp.ToList());
        }

        private void SampleDate()
        {

            Company company = new Company()
            {
                Name = "Рога и копыта"
            };
            _context.Company.Add(company);
            _context.SaveChanges();

            Post post = new Post()
            {
                Name = "Директор"
            };
            _context.Post.Add(post);
            _context.SaveChanges();

            Subdivision subdivision = new Subdivision()
            {
                Name = "Подразделение 1"
            };
            _context.Subdivision.Add(subdivision);
            _context.SaveChanges();

            Employee employee = new Employee()
            {
                Surname = "Иванов",
                Name = "Иван",
                Patronymic = "Ивановвич",
                Email = "ivanov@mail.ru",
                Phone = "89170000000"
            };
            _context.Employee.Add(employee);
            _context.SaveChanges();

            CompEmp compEmp = new CompEmp()
            {
                Company = company,
                Employee = employee,
                Post = post,
                Subdivision = subdivision
            };
            _context.CompEmp.Add(compEmp);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

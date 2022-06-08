using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PEOTest.BLL.DTO;
using PEOTest.BLL.Helper;
using PEOTest.BLL.Infrastructure;
using PEOTest.BLL.Interfaces;
using PEOTest.DAL;
using PEOTest.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
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

        public IEnumerable<CompEmpDTO> GetAllCompEmp()
        {
            if (!_context.CompEmp.Any())
            {
                SampleDate();
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmp, CompEmpDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Company, CompanyDTO>();
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Subdivision, SubdivisionDTO>();
            })
                .CreateMapper();

            return mapper.Map<IEnumerable<CompEmp>, List<CompEmpDTO>>(_context.CompEmp.ToList());
        }

        public IEnumerable<CompEmpDTO> GetAllCompEmp(int companyId, int subdivisionId, int postId,
            string surname, string name, string patronymic,
            string phone, string email,
            string sortName = "Employee.Surname")
        {
            if (!_context.CompEmp.Any())
            {
                SampleDate();
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmp, CompEmpDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Company, CompanyDTO>();
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Subdivision, SubdivisionDTO>();
            })
                .CreateMapper();

            Expression<Func<CompEmp, bool>> predicateWhere = a => 1 == 1;
            if (companyId != 0)
            {
                predicateWhere = predicateWhere.AndAlso(a => a.CompanyId == companyId);
            }
            if (subdivisionId != 0)
            {
                predicateWhere = predicateWhere.AndAlso(a => a.SubdivisionId == subdivisionId);
            }
            if (postId != 0)
            {
                predicateWhere = predicateWhere.AndAlso(a => a.PostId == postId);
            }
            if (surname != "" && surname != null)
            {
                predicateWhere = predicateWhere.AndAlso(a => a.Employee.Surname.ToLower().Contains(surname.ToLower()));
            }
            if (name != "" && name != null)
            {
                predicateWhere = predicateWhere.AndAlso(a => a.Employee.Name.ToLower().Contains(name.ToLower()));
            }
            if (patronymic != "" && patronymic != null)
            {
                predicateWhere = predicateWhere.AndAlso(a => a.Employee.Patronymic.ToLower().Contains(patronymic.ToLower()));
            }
            if (phone != "" && phone != null)
            {
                predicateWhere = predicateWhere.AndAlso(a => a.Employee.Phone.Contains(phone.ToLower()));
            }
            if (email != "" && email != null)
            {
                predicateWhere = predicateWhere.AndAlso(a => a.Employee.Email.ToLower().Contains(email.ToLower()));
            }


            return mapper.Map<IEnumerable<CompEmp>, List<CompEmpDTO>>(_context.CompEmp.Where(predicateWhere).OrderBy(sortName).ToList());
        }

        public CompEmpDTO GetById(int id)
        {
            if (!_context.CompEmp.Any())
            {
                SampleDate();
            }
            if(id == 0)
            {
                throw new ValidationException("Данный пользователь не существует", "");
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmp, CompEmpDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Company, CompanyDTO>();
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Subdivision, SubdivisionDTO>();
            })
                .CreateMapper();
            return mapper.Map<CompEmp, CompEmpDTO>(_context.CompEmp.FirstOrDefault(a => a.Id == id));
        }

        public int CreateComEmp(CompanyDTO companyDTO,
            SubdivisionDTO subdivisionDTO,
            PostDTO postDTO,
            EmployeeDTO employeeDTO)
        {
            CompEmp compEmp = new CompEmp()
            {
                CompanyId = companyDTO.Id,
                SubdivisionId = subdivisionDTO.Id,
                PostId = postDTO.Id,
                EmployeeId = employeeDTO.Id
            };
            _context.CompEmp.Add(compEmp);
            _context.SaveChanges();
            return compEmp.Id;
        }

        public int EditComEmp(int CompEmpId,
            CompanyDTO companyDTO,
            SubdivisionDTO subdivisionDTO,
            PostDTO postDTO,
            EmployeeDTO employeeDTO)
        {
            CompEmp compEmp = new CompEmp()
            {
                Id = CompEmpId,
                CompanyId = companyDTO.Id,
                SubdivisionId = subdivisionDTO.Id,
                PostId = postDTO.Id,
                EmployeeId = employeeDTO.Id
            };
            _context.Entry(compEmp).State = EntityState.Modified;
            _context.SaveChanges();
            return compEmp.Id;
        }

        public void Delete(int id)
        {
            if (id == 0)
            {
                throw new ValidationException("Данный пользователь не существует", "");
            }
            CompEmp compEmp = _context.CompEmp.FirstOrDefault(a => a.Id == id);
            _context.CompEmp.Remove(compEmp);
            _context.SaveChanges();
        }

        private void SampleDate()
        {

            Company company = new Company(){ Name = "Рога и копыта" };
            _context.Company.Add(company);
            _context.SaveChanges();

            Post post = new Post(){ Name = "Директор" };
            _context.Post.Add(post);
            _context.SaveChanges();

            Subdivision subdivision = new Subdivision(){ Name = "Подразделение 1" };
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

            post = new Post() { Name = "Работник" };
            _context.Post.Add(post);
            _context.SaveChanges();

            employee = new Employee()
            {
                Surname = "Петров",
                Name = "Петр",
                Patronymic = "Петрович",
                Email = "petrov@mail.ru",
                Phone = "89170000000"
            };
            _context.Employee.Add(employee);
            _context.SaveChanges();

            compEmp = new CompEmp()
            {
                Company = company,
                Employee = employee,
                Post = post,
                Subdivision = subdivision
            };
            _context.CompEmp.Add(compEmp);
            _context.SaveChanges();

            employee = new Employee()
            {
                Surname = "Новикова",
                Name = "Наталья",
                Patronymic = "Александровна",
                Email = "novikova@mail.ru",
                Phone = "89170000000"
            };
            _context.Employee.Add(employee);
            _context.SaveChanges();

            compEmp = new CompEmp()
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

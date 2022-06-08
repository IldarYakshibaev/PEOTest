using AutoMapper;
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
    public class EmployeeService : IEmployeeService
    {
        public EFDbContext _context;

        public EmployeeService(EFDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CompEmp, CompEmpDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Company, CompanyDTO>();
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<Subdivision, SubdivisionDTO>();
            })
                .CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(_context.Employee.ToList());
        }
        public int Create(EmployeeDTO employeeDTO)
        {
            if (employeeDTO.Surname == "" || employeeDTO.Surname == null)
            {
                throw new ValidationException("Не указана Фамилия", "Surname");
            }
            if (employeeDTO.Name == "" || employeeDTO.Name == null)
            {
                throw new ValidationException("Не указано Имя", "Name");
            }
            if (employeeDTO.Patronymic == "" || employeeDTO.Patronymic == null)
            {
                throw new ValidationException("Не указано Отчество", "Patronymic");
            }
            if (employeeDTO.Phone == "" || employeeDTO.Phone == null)
            {
                throw new ValidationException("Не указан Телефон", "Phone");
            }
            if (_context.Employee.Any(a => a.Phone == employeeDTO.Phone))
            {
                throw new ValidationException("Телефон уже существует", "Phone");
            }
            if (employeeDTO.Email == "" || employeeDTO.Email == null)
            {
                throw new ValidationException("Не указана Почта", "Email");
            }
            if (_context.Employee.Any(a => a.Email == employeeDTO.Email))
            {
                throw new ValidationException("Почта занята", "Email");
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeDTO, Employee>();
            })
                .CreateMapper();
            Employee employee = mapper.Map<EmployeeDTO, Employee>(employeeDTO);

            _context.Employee.Add(employee);
            _context.SaveChanges();

            return employee.Id;

        }
        public int Edit(EmployeeDTO employeeDTO)
        {
            if (employeeDTO.Surname == "" || employeeDTO.Surname == null)
            {
                throw new ValidationException("Не указана Фамилия", "Surname");
            }
            if (employeeDTO.Name == "" || employeeDTO.Name == null)
            {
                throw new ValidationException("Не указано Имя", "Name");
            }
            if (employeeDTO.Patronymic == "" || employeeDTO.Patronymic == null)
            {
                throw new ValidationException("Не указано Отчество", "Patronymic");
            }
            if (employeeDTO.Phone == "" || employeeDTO.Phone == null)
            {
                throw new ValidationException("Не указан Телефон", "Phone");
            }
            if (_context.Employee.Any(a => a.Phone == employeeDTO.Phone && a.Id != employeeDTO.Id))
            {
                throw new ValidationException("Телефон уже существует", "Phone");
            }
            if (employeeDTO.Email == "" || employeeDTO.Email == null)
            {
                throw new ValidationException("Не указана Почта", "Email");
            }
            if (_context.Employee.Any(a => a.Email == employeeDTO.Email && a.Id != employeeDTO.Id))
            {
                throw new ValidationException("Почта занята", "Email");
            }

            var mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeDTO, Employee>();
            })
                .CreateMapper();
            Employee employee = mapper.Map<EmployeeDTO, Employee>(employeeDTO);

            _context.Entry(employee).State = EntityState.Modified;
            _context.SaveChanges();

            return employee.Id;

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

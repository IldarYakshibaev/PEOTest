using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface ICompEmpService
    {
        IEnumerable<CompEmpDTO> GetAllCompEmp();
        IEnumerable<CompEmpDTO> GetAllCompEmp(int companyId, int subdivisionId, int postId,
            string surname, string name, string patronymic,
            string phone, string email,
            string sortName = "Employee.Surname");
        CompEmpDTO GetById(int id);
        int CreateComEmp(CompanyDTO companyDTO, 
            SubdivisionDTO subdivisionDTO, 
            PostDTO postDTO, 
            EmployeeDTO employeeDTO);
        int EditComEmp(int CompEmpId,
            CompanyDTO companyDTO,
            SubdivisionDTO subdivisionDTO,
            PostDTO postDTO,
            EmployeeDTO employeeDTO);
        void Delete(int id);
        void Dispose();
    }
}

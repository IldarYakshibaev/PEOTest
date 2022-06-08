using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> GetAllEmployee();
        int CreateEmployee(EmployeeDTO employeeDTO);
        int EditEmployee(EmployeeDTO employeeDTO);
        void Dispose();
    }
}

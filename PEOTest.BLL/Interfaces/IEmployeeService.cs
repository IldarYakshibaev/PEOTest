using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> GetAll();
        int Create(EmployeeDTO employeeDTO);
        int Edit(EmployeeDTO employeeDTO);
        void Dispose();
    }
}

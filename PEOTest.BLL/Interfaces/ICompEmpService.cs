﻿using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface ICompEmpService
    {
        IEnumerable<CompEmpDTO> GetAllCompEmp();
        int CreateComEmp(CompanyDTO companyDTO, 
            SubdivisionDTO subdivisionDTO, 
            PostDTO postDTO, 
            EmployeeDTO employeeDTO);
        void Dispose();
    }
}

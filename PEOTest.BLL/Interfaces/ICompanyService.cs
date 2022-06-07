using Microsoft.AspNetCore.Mvc.Rendering;
using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDTO> GetAllCompany();
        IEnumerable<SelectListItem> GetAllCompanySL(int companyId = 0);
        int CreateCompany(CompanyDTO companyDTO);
        void Dispose();
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDTO> GetAll();
        CompanyDTO GetById(int id);
        IEnumerable<SelectListItem> GetAllSL(int id = 0);
        int Create(CompanyDTO companyDTO);
        int Edit(CompanyDTO companyDTO);
        void Dispose();
    }
}

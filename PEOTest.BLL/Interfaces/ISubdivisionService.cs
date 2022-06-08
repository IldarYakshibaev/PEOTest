using Microsoft.AspNetCore.Mvc.Rendering;
using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface ISubdivisionService
    {
        IEnumerable<SubdivisionDTO> GetAll();
        SubdivisionDTO GetById(int id);
        IEnumerable<SelectListItem> GetAllSL(int id = 0);
        int Create(SubdivisionDTO subdivisionDTO);
        int Edit(SubdivisionDTO subdivisionDTO);
        void Dispose();
    }
}

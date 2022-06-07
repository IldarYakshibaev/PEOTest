using Microsoft.AspNetCore.Mvc.Rendering;
using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface ISubdivisionService
    {
        IEnumerable<SubdivisionDTO> GetAllSubdivision();
        IEnumerable<SelectListItem> GetAllSubdivisionSL(int subdivisionId = 0);
        int CreateSubdivision(SubdivisionDTO subdivisionDTO);
        void Dispose();
    }
}

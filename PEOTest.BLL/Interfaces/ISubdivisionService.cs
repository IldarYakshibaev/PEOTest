using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface ISubdivisionService
    {
        IEnumerable<SubdivisionDTO> GetAllSubdivision();
        void Dispose();
    }
}

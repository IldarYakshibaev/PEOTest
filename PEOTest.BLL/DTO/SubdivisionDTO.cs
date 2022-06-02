﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.DTO
{
    public class SubdivisionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompEmpDTO> CompEmpDTOs { get; set; }

        public SubdivisionDTO()
        {
            CompEmpDTOs = new List<CompEmpDTO>();
        }
    }
}

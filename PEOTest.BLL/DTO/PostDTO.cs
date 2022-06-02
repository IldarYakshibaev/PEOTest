﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompEmpDTO> CompEmpDTOs { get; set; }

        public PostDTO()
        {
            CompEmpDTOs = new List<CompEmpDTO>();
        }
    }
}

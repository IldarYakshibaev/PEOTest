using Microsoft.AspNetCore.Mvc.Rendering;
using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetAll();
        PostDTO GetById(int id);
        IEnumerable<SelectListItem> GetAllSL(int id = 0);
        int Create(PostDTO postDTO);
        int Edit(PostDTO postDTO);
        void Dispose();
    }
}

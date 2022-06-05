using Microsoft.AspNetCore.Mvc.Rendering;
using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetAllPost();
        List<SelectListItem> GetAllPostSL();
        int CreatePost(PostDTO postDTO);
        void Dispose();
    }
}

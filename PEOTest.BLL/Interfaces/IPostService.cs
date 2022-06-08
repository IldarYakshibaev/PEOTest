﻿using Microsoft.AspNetCore.Mvc.Rendering;
using PEOTest.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PEOTest.BLL.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostDTO> GetAllPost();
        PostDTO GetById(int id);
        IEnumerable<SelectListItem> GetAllPostSL(int postId = 0);
        int CreatePost(PostDTO postDTO);
        int Edit(PostDTO postDTO);
        void Dispose();
    }
}

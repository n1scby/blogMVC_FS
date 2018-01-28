using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IBlogRepository
    {
        void Add(Blog newBlog);
        void Delete(Blog deleteBlog);
        Blog GetBlogById(int id);
        void Edit(Blog updatedBlog);
        List<Blog> GetBlogList();
        
    }
}

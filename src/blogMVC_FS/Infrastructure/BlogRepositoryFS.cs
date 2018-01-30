using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class BlogRepositoryFS : IBlogRepository
    {
        private static List<Blog> _blogList;
        private static int _nextNum = 1;

        private const string PATH = "Data";
        private const string FILENAME = "blogData.json";

        private readonly string _fileFullPath = Path.Combine(PATH, FILENAME);

        public BlogRepositoryFS()
        {


            if (_blogList == null)
            {
                _blogList = LoadFile();

                if (_blogList.Count > 0)
                {
                    _nextNum = _blogList.Max(m => m.Id) + 1;
                }

            }

        }

        public void Add(Blog newBlog)
        {
            newBlog.Id = _nextNum++;
            _blogList.Add(newBlog);
            SaveFile();
        }

        public void Delete(Blog deleteBlog)
        {
            Blog removeBlog = GetBlogById(deleteBlog.Id);
            _blogList.Remove(removeBlog);
            SaveFile();
        }

        public void Edit(Blog updatedBlog)
        {
            Blog originalBlog = GetBlogById(updatedBlog.Id);
            originalBlog.Content = updatedBlog.Content;
            originalBlog.Summary = updatedBlog.Summary;
            originalBlog.Title = updatedBlog.Title;
            originalBlog.Image = updatedBlog.Image;
            originalBlog.AboutImage = updatedBlog.AboutImage;
            originalBlog.Author = updatedBlog.Author;
            originalBlog.Date = updatedBlog.Date;
            originalBlog.Scripts = updatedBlog.Scripts;
            SaveFile();
        }

        public Blog GetBlogById(int id)
        {
            return _blogList.Find(b => b.Id == id);
        }

        public List<Blog> GetBlogList()
        {
            return _blogList;
        }

        public List<Blog> LoadFile()
        {
            try
            {
                string rawListStr = File.ReadAllText(_fileFullPath);

                List<Blog> rawList = JsonConvert.DeserializeObject<List<Blog>>(rawListStr);

                return rawList;
            }
            catch (Exception ex)
            {
                // TODO Log the exception

                return new List<Blog>();
            }
        }


        public void SaveFile()
        {
            try
            {
                if (!Directory.Exists(PATH))
                {
                    Directory.CreateDirectory(PATH);
                }

                string rawListStr = JsonConvert.SerializeObject(_blogList);

                File.WriteAllText(_fileFullPath, rawListStr);
            }
            catch (Exception ex)
            {
                // TODO Log the exception
            }
        }


    }
}

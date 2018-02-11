using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure
{
    public class BlogRepositoryADO : IBlogRepository
    {
        private string _connectionString;
        private string selectQuery = "SELECT Id, Title, Author, Date, Summary, Content, Image, AboutImage, Scripts, CSS FROM Blog \n";
        private string byId = "WHERE Id = @id";
        private string deleteQuery = "DELETE Blog \n";
        private string updateQuery = "UPDATE Blog SET Title = @title, Author = @author, Date = @date, Summary = @summary, Content = @Content, Image = @image, AboutImage = @aboutImage, Scripts = @scripts, CSS = @css \n";
        private string insertQuery = "INSERT into Blog (Title, Author, Date, Summary, Content, Image, AboutImage, Scripts, CSS) values(@title, @author, @date, @summary, @content, @image, @aboutImage, @scripts, @css)";


        public BlogRepositoryADO(string connString)
        {
            _connectionString = connString; 
        }

        public void Add(Blog newBlog)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@title", newBlog.Title);
                cmd.Parameters.AddWithValue("@author", newBlog.Author ?? "");
                cmd.Parameters.AddWithValue("@date", newBlog.Date);
                cmd.Parameters.AddWithValue("@summary", newBlog.Summary);
                cmd.Parameters.AddWithValue("@content", newBlog.Content);
                cmd.Parameters.AddWithValue("@image", newBlog.Image ?? "");
                cmd.Parameters.AddWithValue("@aboutImage", newBlog.AboutImage ?? "");
                cmd.Parameters.AddWithValue("@scripts", newBlog.Scripts ?? "");
                cmd.Parameters.AddWithValue("@css", newBlog.CSS ?? "");

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }


            }
        }

        public void Delete(Blog deleteBlog)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(deleteQuery + byId, conn);
                cmd.Parameters.AddWithValue("@id", GetBlogById(deleteBlog.Id));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }


            }
        }

        public void Edit(Blog updatedBlog)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(updateQuery + byId, conn);
                cmd.Parameters.AddWithValue("@title", updatedBlog.Title);
                cmd.Parameters.AddWithValue("@author", updatedBlog.Author ?? "");
                cmd.Parameters.AddWithValue("@date", updatedBlog.Date);
                cmd.Parameters.AddWithValue("@summary", updatedBlog.Summary);
                cmd.Parameters.AddWithValue("@content", updatedBlog.Content);
                cmd.Parameters.AddWithValue("@image", updatedBlog.Image ?? "");
                cmd.Parameters.AddWithValue("@aboutImage", updatedBlog.AboutImage ?? "");
                cmd.Parameters.AddWithValue("@scripts", updatedBlog.Scripts ?? "");
                cmd.Parameters.AddWithValue("@css", updatedBlog.CSS ?? "");
                cmd.Parameters.AddWithValue("@id", updatedBlog.Id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

            }
        }

        public Blog GetBlogById(int id)
        {
            Blog blog = new Blog();
            using (SqlConnection conn = new SqlConnection(_connectionString)){

                SqlCommand cmd = new SqlCommand(selectQuery + byId, conn);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        blog = new Blog()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Title = reader[1].ToString(),
                            Author = reader[2].ToString(),
                            Date = DateTime.Parse(reader[3].ToString()),
                            Summary = reader[4].ToString(),
                            Content = reader[5].ToString(),
                            Image = reader[6].ToString(),
                            AboutImage = reader[7].ToString(),
                            Scripts = reader[8].ToString(),
                            CSS = reader[9].ToString()


                        };


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            return blog;

        }

        public List<Blog> GetBlogList()
        {
            List<Blog> blogList = new List<Blog>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(selectQuery, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Title = reader[1].ToString(),
                            Author = reader[2].ToString(),
                            Date = DateTime.Parse(reader[3].ToString()),
                            Summary = reader[4].ToString(),
                            Content = reader[5].ToString(),
                            Image = reader[6].ToString(),
                            AboutImage = reader[7].ToString(),
                            Scripts = reader[8].ToString(),
                            CSS = reader[9].ToString()


                        };

                        blogList.Add(blog);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }


            return blogList;
        }
        
          


        
    }
}

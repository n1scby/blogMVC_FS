using ApplicationCore.Entities;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConvertFStoDB
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }


        public static void Main(string[] args)
        {
            string connectionString = GetConnectionString();

            // Create Both Repositories
            BlogRepositoryADO blogRepoADO = new BlogRepositoryADO(connectionString);
            BlogRepositoryFS blogRepoFS = new BlogRepositoryFS();


            // Get List of Blog Posts from FS and loop through the list.  Add each Post to the database.

            blogRepoFS.LoadFile();
            List<Blog> blogList = blogRepoFS.GetBlogList();
            foreach(Blog post in blogList)
            {
                blogRepoADO.Add(post);
            }

            Console.WriteLine("Conversion Complete.");
        }


        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string connectionString = (Configuration.GetConnectionString("DefaultConnection"));

            return connectionString;
        }
    }
}

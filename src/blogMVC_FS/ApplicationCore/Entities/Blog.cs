using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string AboutImage { get; set; }
        public string Scripts { get; set; }
        public string CSS { get; set; }

    }
}

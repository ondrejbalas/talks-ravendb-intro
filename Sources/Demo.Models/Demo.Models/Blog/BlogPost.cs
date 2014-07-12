using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Models.Blog
{
    public class BlogPost
    {
        public string Title { get; set; }
        public List<string> Tags { get; set; }

        public BlogPost()
        {
            Tags = new List<string>();
        }
    }
}

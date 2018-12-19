using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Models
{
    public class MyBlogContext : DbContext
    {
        public MyBlogContext (DbContextOptions<MyBlogContext> options)
            : base(options)
        {
        }

        public DbSet<MyBlog.Models.BlogPost> BlogPost { get; set; }
    }
}

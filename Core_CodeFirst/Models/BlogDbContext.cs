//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Core_CodeFirst.Models;

public class BlogDbContext : DbContext
{

    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
    {

    }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Blog> Blogs { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    //public virtual DbSet<Game> Games { get; set; }
}





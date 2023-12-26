//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Core_CodeFirst.Models;

public class ComicDbContext : DbContext
{

    public ComicDbContext(DbContextOptions<ComicDbContext> options) : base(options)
    {

    }
    //::new 2023-12-5
    public virtual DbSet<Maker> Makers { get; set; }
    public virtual DbSet<Comic> Comics { get; set; }
    //::<timmy><2023-12-12><added>
    public virtual DbSet<Admin> Admins { get; set; }

    //::auto generated for Score ctrl
    //public DbSet<Core_CodeFirst.Models.Score> Score { get; set; }


    //::study
    //public string GetMySessionShare(string key)
    //{
    //    string _str = null;
    //    try
    //    {
    //        byte[] bv = null;
    //        //Microsoft.AspNetCore.Http.HttpContext hp = new Microsoft.AspNetCore.Http.HttpContext();s
    //        HttpContext.Session.TryGetValue(key, out bv);
    //        //::byte[] to string
    //        _str = System.Text.Encoding.Default.GetString(bv);
    //    }
    //    catch
    //    {
    //    }
    //    return _str;
    //}

}





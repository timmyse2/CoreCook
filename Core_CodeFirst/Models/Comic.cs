using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Core_CodeFirst.Models
{
    public class Comic
    {
        public int ComicId { get; set; } //PK
        public string ComicName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int MakerId { get; set; } //FK

        //[ForeignKey("MakerId")]                
        public virtual Maker Maker { get; set; }

        //::my test for api sharing
        //public string GetSessionTest(string key)
        //{
        //    string _str = null;
        //    try
        //    {
        //        byte[] bv = null;
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
}

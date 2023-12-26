using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core_CodeFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public string G_GetMySession(string key)
        {
            string _str = null;
            try
            {
            //    byte[] bv = null;
            //    HttpContext.Session.TryGetValue(key, out bv);
            //    //::byte[] to string
            //    _str = System.Text.Encoding.Default.GetString(bv);
            }
            catch
            {
            }
            return _str;
        }
    }
}

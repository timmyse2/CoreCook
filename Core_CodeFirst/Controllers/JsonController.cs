using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core_CodeFirst.Controllers
{
    public class JsonController : Controller
    {
        public IActionResult Index()
        {
            //return View();
            return RedirectToAction("jcmd");
        }


        public IActionResult jcmd()
        {
            //::refer to p9-16

            List<Models.Person> persons = new List<Models.Person> {
                new Models.Person{Id=1,Name="Saber"}
                ,new Models.Person{Id=2,Name="Lancer"}                
            };

            //1. System.Text.Json (Core 3.0)
            //string json1 = System.Text.JSON
            //var list1;

            //2-Newtonsoft
            string json2 = 
                Newtonsoft.Json.JsonConvert.SerializeObject(persons);
            var list2 =
                Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<Models.Person>>(json2);

            //3. Controller
            JsonResult json3 = Json(persons);

            //4. Utf8Json (3rd party)
            //Utf8Json

            //5. Jil (3rd party)
            

            return View(persons);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Core_CodeFirst.Models; //::models class

namespace Core_CodeFirst.Controllers
{
    public class JsonController : Controller
    {
        public IActionResult Index()
        {
            //return View();
            return RedirectToAction("jcmd");
        }


        public IActionResult Jcmd()
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

        public IActionResult GetTemp()
        {
            string[] Labels = {"JAN","FEB","MAR","AP","MAY","JUN","JUL","AUG","SEP","OCT","NOV","DEC" };

            var jsonLables =
                Newtonsoft.Json.JsonConvert.SerializeObject(Labels);

            ViewData["JsonLabels"] = jsonLables;

            List<Location> Locs = new List<Location>
            {
                new Location {City="NewYork", Temperature =new double[]{0,5,3,0.5,9,7.2,12,0,0,0,1,-2}}
                ,new Location {City="Taipei", Temperature =new double[]{15,20,25,23,20,29,27,22,20,10,12,11}}
                ,new Location {City="Tokyo", Temperature =new double[]{10,15,13,10,19,11,12,10,5,7,2,1}}
            };

            string jsonLocations = Newtonsoft.Json.JsonConvert.SerializeObject(Locs);
            ViewData["JsonLocations"] = jsonLocations;

            return View(Locs);
        }

        //::book c9 p26
        //public IActionResult GetScaleNumber()
        //public ActionResult GetScaleNumber()
        public ActionResult<IEnumerable<Utility.CarScales>> GetScaleNumber()
        {
            List<Utility.CarScales> carScaleNumbers = new List<Utility.CarScales>
            {
                new Utility.CarScales{
                    Id =1, Car="BMW"
                    ,Scalesdata =new int[]{120,112,230,110,123,123,33,22,22,11,22 }}
                ,
                new Utility.CarScales{
                    Id =2, Car="BENZ"
                    ,Scalesdata =new int[]{120,112,230,110,13,133,33,22,22,11,223 }}
                ,
                new Utility.CarScales{
                    Id =3, Car="Toyota"
                    ,Scalesdata =new int[]{10,12,130,1110,13,223,33,22,22,11,22 }}
            };


            //return View();
            return carScaleNumbers;
            //return Content("carScaleNumbers " + carScaleNumbers);
        }


    }
}
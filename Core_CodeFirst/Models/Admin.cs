using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_CodeFirst.Models
{
    public class Admin
    {
        public int Id { get; set; } //PK
        public string Account { get; set; }
        //public string Admin { get; set; }
        public string Pwd { get; set; }
        public string Pin { get; set; }
    }
}

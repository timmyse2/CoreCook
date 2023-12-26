using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_CodeFirst.Models
{
    public class Score
    {
        //::refer to book core7 p7-23

        public int Id { get; set; }
        public string Name { get; set; }
        public int Chinese { get; set; }
        public int English { get; set; }
        public int Math { get; set; }
        public int Sport { get; set; }
        public int Art { get; set; }

        public int Total { get; set; } //
    }
}

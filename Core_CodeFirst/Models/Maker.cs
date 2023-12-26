using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_CodeFirst.Models
{
    public class Maker
    {
        //[Key]
        public int MakerId { get; set; } //UserId [Primary Key]
        public string UserName { get; set; }
        public string Image { get; set; }

        //::navigation property
        public virtual ICollection<Comic> Comic { get; set; }
    }
}

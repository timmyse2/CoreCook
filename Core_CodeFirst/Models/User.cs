using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_CodeFirst.Models
{
    public class User
    {
        public int Id { get; set; } //UserId [Primary Key]
        public string UserName { get; set; }
        public string Email { get; set; }

        ////public int Total { get; set; } // new type

        //::navigation property...
        public virtual ICollection<Blog> vt_Blogs { get; set; }

        //public virtual ICollection<Post> vt_Posts { get; set; }

        //::my study
        //public virtual ICollection<Game> Games { get; set; }
    }
}

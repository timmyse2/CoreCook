using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_CodeFirst.Models
{
    public class Post
    {
        public int PostId { get; set; }  //::PK
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; } //::FK

        //:: navigation property
        public virtual Blog Blog { get; set; }

        //:: try to make more one
        //public virtual Blog virtualBlog { get; set; }
    }
}

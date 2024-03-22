using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Core_CodeFirst.Models
{
    public class Blog
    {
        //public int Id { get; set; } //BlogId [Primary Key]
        public int BlogId { get; set; } //BlogId [Primary Key]
        public string BlogName { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; } //foreign key

        //::Navigation
        //[ForeignKey("UserId")]        
        public virtual User User { get; set; }  //with FK 
        
        public virtual ICollection<Post> Post { get; set; }
    }
}

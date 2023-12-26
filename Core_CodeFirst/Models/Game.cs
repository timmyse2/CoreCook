using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_CodeFirst.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string MISC { get; set; }

        public int UserID { get; set; }

    }
}

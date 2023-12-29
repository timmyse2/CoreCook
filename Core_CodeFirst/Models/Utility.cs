using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_CodeFirst.Models
{
    public class Utility //: IUtility ???
    {
        public class CarScales
        {
            public int Id { get; set; }
            public string Car { get; set; }
            public int[] Scalesdata { get; set; }

            //move to here from outside
            //public int[] GetNumbers(int num)
            //{

            //    Random rdn = new Random(Guid.NewGuid().GetHashCode());
            //    int[] Numbs = new int[num];

            //    for (int i = 0; i < num; i++)
            //    {
            //        Numbs[i] = rdn.Next(1, 500);
            //    }

            //    //var array = new int[]
            //    //{
            //    //    9,5,2,7
            //    //};

            //    var array = Enumerable.Range(1, num)
            //        .Select(x => rdn.Next(1, 500)).ToArray();


            //    rdn = null;
            //    return array;
            //}
        }

        //:: book c9-5-p25
        public int[] GetNumbers(int num)
        {

            Random rdn = new Random(Guid.NewGuid().GetHashCode());
            int[] Numbs = new int[num];

            for(int i=0; i < num; i++)
            {
                Numbs[i] = rdn.Next(1, 500);
            }

            //var array = new int[]
            //{
            //    9,5,2,7
            //};

            var array = Enumerable.Range(1, num)
                .Select(x => rdn.Next(1,500)).ToArray();


            rdn = null;
            return array;
        }
    }
}

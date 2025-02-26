using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_class9
{
    public class MyArray
    {
        public MyArray(params int[] numbers) // необмежена кількість параметрів
        {
            _arr = numbers; 
        }

        private int[] _arr;

        public int this[int index]
        {
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public  class MathOperations
    {   
        public int Sum(params int[] array)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++) 
            {
               sum += array[i];
            }
          
                
            return sum;

        }
    }

}

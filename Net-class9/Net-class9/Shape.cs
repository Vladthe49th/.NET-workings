using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_class9
{
    public abstract class Shape
    {
        public abstract double GetArea();

        public abstract double Perimeter { get; set; }   
    }
}

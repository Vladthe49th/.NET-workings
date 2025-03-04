using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Class11
{
  
    class Program
    {
        static void Main()
        {

          
        }





        static int DaystoWrite(int WordsLeft, int WordsPerDay)
        {
 
            if (WordsLeft <= 0)
            {
                return 0;
            }

            return 1 + DaystoWrite(WordsLeft - WordsPerDay, WordsPerDay);
        }

    }
}

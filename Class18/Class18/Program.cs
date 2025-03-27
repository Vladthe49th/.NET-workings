using System.Linq;

namespace Class18
{
    class Program
    {
        static int[] GetCommonElems(int[] array1, int[] array2)
        {
            var elemCount = new Dictionary<int, int>();
            var result = new List<int>();

            foreach (var num in array1)
            {
                if (elemCount.ContainsKey(num))
                    elemCount[num]++;
                else
                    elemCount[num] = 1;
            }

            foreach (var num in array2)
            {
                if (elemCount.ContainsKey(num) && elemCount[num] > 0)
                {
                    result.Add(num);
                    elemCount[num]--;
                }
            }

            return result.ToArray();
        }



        static void Main()
        {

            int[] array1 = { 1, 2, 3, 4, 5, 2, 3 };
            int[] array2 = { 3, 4, 4, 2, 2 };

            int[] commonElements = GetCommonElems(array1, array2);
            Console.WriteLine(string.Join(", ", commonElements));
        }

        
        
    }
   
}


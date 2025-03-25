using System;
using System.Collections.Generic;
using System.Linq;

namespace Class17
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string folderPath = @"C:\path\to\folder"; 
            int totalQuantity = 0;

            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath, "*.txt"); // Getting all the text files

                foreach (string file in files)
                {
                    string[] lines = File.ReadAllLines(file); 

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(','); 
                        if (parts.Length == 2 && int.TryParse(parts[1], out int quantity))
                        {
                            totalQuantity += quantity; 
                        }
                    }
                }

                Console.WriteLine($"Total quantity of sold products: {totalQuantity}");
            }
            else
            {
                Console.WriteLine("Folder doesn`t exist!");
            }


        }

       
    }
}

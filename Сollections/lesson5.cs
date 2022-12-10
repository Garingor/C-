using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] firstArray = {"3", "1", "4", "2", "2"};
            string[] secondArray = {"3", "3", "4", "5"};
            List<string> finalList = new List<string>();

            CombineInCollection(finalList, firstArray);
            CombineInCollection(finalList, secondArray);
            
            foreach (var value in finalList)
            {
                Console.Write(value + " ");
            }
        }
        
        static void CombineInCollection(List<string> finalList, string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (!finalList.Contains(array[i]))
                {
                    finalList.Add(array[i]);
                }
            }
        }
    }
}
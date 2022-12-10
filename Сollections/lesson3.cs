using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandExit = "exit";
            const string CommandSum = "sum";
            
            bool isExit = false;
            string choose = "";

            List<int> array = new List<int>();
            
            while (isExit == false)
            {
                Console.Write("Введите команду/число: ");

                choose = Console.ReadLine();
                
                switch (choose)
                {
                    case CommandSum:
                        int sum = 0;

                        foreach (int element in array)
                        {
                            sum += element;
                        }
                        
                        Console.WriteLine($"сумма - {sum}");
                        break;
                    case CommandExit:
                        isExit = true;
                        break;
                    default:
                        bool isNumber = int.TryParse(choose, out int number);
                        
                        if (isNumber == true)
                        {
                            array.Add(number);
                        }
                        else
                        {
                            Console.WriteLine("команда не распознана");
                        }
                        break;
                }
            }
        }
    }
}
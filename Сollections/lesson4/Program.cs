using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int CommandAddInformation = 1;
            const int CommandPrintInformation = 2;
            const int CommandDeleteInformation = 3;
            const int CommandExit = 4;

            Dictionary<string, string> workers = new Dictionary<string, string>();

            bool isExit = false;
            string choose;
            
            while (isExit == false)
            {
                Console.Write($"\n{CommandAddInformation} - добавить досье" +
                              $"\n{CommandPrintInformation} - вывести все досье" +
                              $"\n{CommandDeleteInformation} - удалить досье" +
                              $"\n{CommandExit} - выход" +
                              $"\nвведите команду: ");

                choose = Console.ReadLine();

                bool isNumber = int.TryParse(choose, out int number);
                        
                if (isNumber == false)
                {
                    Console.WriteLine("команда не распознана");
                }
                else
                {
                    switch (number)
                    {
                        case CommandAddInformation:
                            AddInformation(workers);
                            break;
                        case CommandPrintInformation:
                            PrintInformation(workers);
                            break;
                        case CommandDeleteInformation:
                            DeleteInformation(workers);
                            break;
                        case CommandExit:
                            isExit = true;
                            break;
                    }
                }
            }
        }
        
        static void AddInformation(Dictionary<string, string> workers)
        {
            const int LengthFullName = 3;
            
            Console.Write("введите ФИО: ");
            string fullName = Console.ReadLine();

            if (workers.ContainsKey(fullName) == true)
            {
                Console.WriteLine("ФИО уже есть в списке");
                return;
            }
            
            if (fullName.Split(" ").Length != LengthFullName)
            {
                Console.WriteLine("неправильно введено ФИО");
                return;
            }
            
            Console.Write("введите должность: ");
            string position = Console.ReadLine();

            workers.Add(fullName, position);
        }
        
        static void PrintInformation(Dictionary<string, string> workers)
        {
            int numberWorkerInList = 1;
            
            foreach (var worker in workers)
            {
                Console.WriteLine($"№{numberWorkerInList} {worker.Key} - {worker.Value}");
                numberWorkerInList++;
            }
        }
        
        static void DeleteInformation(Dictionary<string, string> workers)
        {
            Console.Write("введите ФИО: ");
            string fullName = Console.ReadLine();
            
            if (workers.ContainsKey(fullName) == false)
            {
                Console.WriteLine("такого ФИО нет в списках");
                return;
            }
            
            workers.Remove(fullName);
        }
    }
}
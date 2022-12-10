using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {"программа", "План действий, задание для вычислительной машины или механизмов с программным " +
                              "обеспечением; описание на специальном языке последовательности выполнения " +
                              "такого плана, задания"},
                {"арбуз","Стелющееся растение сем. тыквенных, культивируемое на бахчах"},
                {"автомобиль","Самоходная машина с двигателем внутреннего сгорания для перевозки " +
                    "пассажиров и грузов по безрельсовым дорогам"}
            };

            bool isExit = false;

            while (!isExit)
            {
                Console.Write("введите слово: ");
                string word = Console.ReadLine();
                WriteMeaningWord(dictionary, word);
            }
        }

        static void WriteMeaningWord(Dictionary<string, string> dictionary, string word)
        {
            if(dictionary.ContainsKey(word.ToLower()))
            {
                Console.WriteLine($"{word.ToLower()} - {dictionary[word.ToLower()]}");
            }
            else
            {
                Console.WriteLine("такого слова нет в словаре");
            }
        }
    }
}
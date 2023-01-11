using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            PoliceStation policeStation = new PoliceStation();
            
            policeStation.ChooseCriminals();
        }
    }

    class Criminal
    {
        public string FullName { get; private set; }
        public bool IsArrest { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public string Nationality { get; private set; }
        
        public Criminal(string fullName, bool isArrest, int height, int weight, string nationality)
        {
            FullName = fullName;
            IsArrest = isArrest;
            Height = height;
            Weight = weight;
            Nationality = nationality;
        }
    }

    class PoliceStation
    {
        private List<Criminal> _criminals;

        public PoliceStation()
        {
            _criminals = new List<Criminal>();
            AddRandomCriminals(_criminals);
        }

        public void ChooseCriminals()
        {
            int minHeight;
            int maxHeight;
            int minWeight;
            int maxWeight;
            string nationality;
            
            Console.WriteLine("Введите минимальный рост: ");

            if (!int.TryParse(Console.ReadLine(), out minHeight) && minHeight > 0)
            {
                Console.WriteLine("некорректно введен рост");
                return;
            }
            
            Console.WriteLine("Введите максимальный рост: ");
            
            if (!int.TryParse(Console.ReadLine(), out maxHeight) && maxHeight > 0 && maxHeight > minHeight)
            {
                Console.WriteLine("некорректно введен рост");
                return;
            }
            
            Console.WriteLine("Введите минимальный вес: ");
            
            if (!int.TryParse(Console.ReadLine(), out minWeight) && minWeight > 0)
            {
                Console.WriteLine("некорректно введен вес");
                return;
            }
            
            Console.WriteLine("Введите максимальный вес: ");
            
            if (!int.TryParse(Console.ReadLine(), out maxWeight) && maxWeight > 0 && maxWeight > minWeight)
            {
                Console.WriteLine("некорректно введен вес");
                return;
            }
            
            Console.WriteLine("Введите национальность: ");

            nationality = Console.ReadLine();

            var filteredCriminals = _criminals.Where(
                criminal => criminal.Height >= minHeight &&
                            criminal.Height <= maxHeight &&
                            criminal.Weight >= minWeight &&
                            criminal.Weight <= maxWeight &&
                            criminal.IsArrest == false &&
                            criminal.Nationality.ToLower() == nationality.ToLower());

            foreach (var criminal in filteredCriminals)
            {
                Console.WriteLine($"ФИО - {criminal.FullName} рост - {criminal.Height} вес - {criminal.Weight} " +
                                  $"национальность - {criminal.Nationality}");
            }
        }
        
        private void AddRandomCriminals(List<Criminal> criminals)
        {
            Random random = new Random();

            int minHeight = 150;
            int maxHeight = 200;
            int minWeight = 50;
            int maxWeight = 120;
            int maxBool = 2;

            List<string> nationalities = new List<string>
            {
                "Бразилец",
                "Мексиканец",
                "Немец",
                "Русский"
            };
            
            List<string> fullNames = new List<string>
            {
                "Морозов Лаврентий Тимофеевич",
                "Матвеев Осип Александрович",
                "Моисеев Прохор Всеволодович",
                "Селезнёв Азарий Лукьевич"
            };

            for (int i = 0; i < fullNames.Count; i++)
            {
                criminals.Add(new Criminal(fullNames[i],Convert.ToBoolean(random.Next(maxBool)),
                    random.Next(minHeight, maxHeight),random.Next(minWeight, maxWeight),
                    nationalities[random.Next(nationalities.Count)]));
            }
        }
    }
}
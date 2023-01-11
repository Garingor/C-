using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Hospital hospital = new Hospital();
            
            hospital.Work();
        }
    }

    class Sick
    {
        public string FullName { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }

        public Sick(string fullName, int age, string disease)
        {
            FullName = fullName;
            Age = age;
            Disease = disease;
        }
    }

    class Hospital
    {
        private List<Sick> _sicks;

        public Hospital()
        {
            _sicks = new List<Sick>();
            AddRandomSicks(_sicks);
        }

        public void Work()
        {
            const string CommandSortByFullName = "1";
            const string CommandSortByAge  = "2";
            const string CommandShowSickWithDisease = "3";
            const string CommandExit = "0";
        
            bool isExit = false;
            
            while (isExit == false)
            {
                Console.Write($"\n{CommandSortByFullName} - отсортировать всех больных по фио" +
                              $"\n{CommandSortByAge} - отсортировать всех больных по возрасту" +
                              $"\n{CommandShowSickWithDisease} - вывести больных с определенным заболеванием" +
                              $"\n{CommandExit} - выход из программы" +
                              "\nВыберите команду ");

                string choose = Console.ReadLine();

                switch (choose)
                {
                    case CommandSortByFullName:
                        SortByFullName();
                        break;
                    case CommandSortByAge:
                        SortByAge();
                        break;
                    case CommandShowSickWithDisease:
                        ShowSickWithDisease();
                        break;
                    case CommandExit:
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("команда не распознана, попробуйте еще раз");
                        break;
                }
            }
        }

        private void SortByFullName()
        {
            List<Sick> sortedSicks = _sicks.OrderBy(sick => sick.FullName).ToList();

            ShowSicks(sortedSicks);
        }
        
        private void SortByAge()
        {
            List<Sick> sortedSicks = _sicks.OrderBy(sick => sick.Age).ToList();
            
            ShowSicks(sortedSicks);
        }

        private void ShowSickWithDisease()
        {
            Console.WriteLine("введите заболевание:");

            string disease = Console.ReadLine();

            List<Sick> sicksWithDisease = _sicks.Where(sick => sick.Disease.ToLower() == disease.ToLower()).ToList();

            ShowSicks(sicksWithDisease);
        }

        private void AddRandomSicks(List<Sick> sicks)
        {
            Random random = new Random();
            
            int minAge = 18;
            int maxAge = 85;
            
            List<string> fullnames = new List<string>
            {
                "Субботин Аверкий Мэлсович",
                "Герасимов Нелли Яковлевич",
                "Наумов Донат Семенович",
                "Сорокин Илларион Станиславович",
                "Лыткин Флор Алексеевич",
                "Харитонов Макар Филатович",
                "Кудряшов Гарри Тимурович",
                "Карпов Самуил Геласьевич",
                "Фролов Родион Германнович",
                "Богданов Донат Куприянович"
            };

            List<string> diseases = new List<string>
            {
                "Артроз",
                "Аскаридоз",
                "Астигматизм",
                "Бессонница",
                "Аппендицит",
                "Зуд",
                "Краснуха",
                "Ларингит",
                "Полиомиелит",
                "Проктит"
            };
            
            for (int i = 0; i < fullnames.Count; i++)
            {
                sicks.Add(new Sick(fullnames[i], random.Next(minAge, maxAge), 
                    diseases[random.Next(diseases.Count)]));
            }
        }

        private void ShowSicks(List<Sick> sicks)
        {
            Console.WriteLine("---------------------------------------------------------------");
            
            foreach (var sick in sicks)
            {
                Console.WriteLine($"ФИО - {sick.FullName} Возраст - {sick.Age} Заболевание - {sick.Disease}");
            }
        }
    }
}
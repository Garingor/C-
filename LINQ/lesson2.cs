using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            BorderService borderService = new BorderService();
            
            borderService.AmnestyForPrisoners();
        }
    }

    class Prisoner
    {
        public string FullName { get; private set; }
        public string Crime { get; private set; }

        public Prisoner(string fullName, string crime)
        {
            FullName = fullName;
            Crime = crime;
        }
    }

    class BorderService
    {
        private List<Prisoner> _prisoners;

        public BorderService()
        {
            _prisoners = new List<Prisoner>();
            AddRandomPrisoners(_prisoners);
        }

        public void AddRandomPrisoners(List<Prisoner> prisoners)
        {
            Random random = new Random();
            
            List<string> fullNames = new List<string>
            {
                "Фомин Антон Аркадьевич",
                "Котов Ян Михайлович",
                "Белоусов Виталий Павлович",
                "Ильин Бронислав Тимурович",
                "Соловьёв Архип Андреевич",
                "Котов Евгений Денисович"
            };
            
            List<string> crimes = new List<string>
            {
                "Антиправительственное",
                "Кража",
                "Мошенничество"
            };

            for (int i = 0; i < fullNames.Count; i++)
            {
                prisoners.Add(new Prisoner(fullNames[i], crimes[random.Next(crimes.Count)]));
            }
        }

        public void AmnestyForPrisoners()
        {
            ShowСitizens(_prisoners);

            _prisoners = _prisoners.Where(prisoner => prisoner.Crime != "Антиправительственное").ToList();

            ShowСitizens(_prisoners);
        }

        private void ShowСitizens(List<Prisoner> prisoners)
        {
            Console.WriteLine("_____________________________");
            
            foreach (var prisoner in prisoners)
            {
                Console.WriteLine($"ФИО - {prisoner.FullName} Преступление - {prisoner.Crime}");
            }
            
        }
    }
}
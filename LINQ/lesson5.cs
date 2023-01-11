using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Storage storage = new Storage();
            
            storage.CheckExpiredStews();
        }
    }

    class Stew
    {
        public string Name { get; private set; }
        public int ProductionYear { get; private set; }
        public int ShelfLife { get; private set; }

        public Stew(string name, int productionYear, int shelfLife)
        {
            Name = name;
            ProductionYear = productionYear;
            ShelfLife = shelfLife;
        }
    }

    class Storage
    {
        private List<Stew> _stews;

        public Storage()
        {
            _stews = new List<Stew>();
            AddRandomStews(_stews);
        }

        public void CheckExpiredStews()
        {
            int currentYear = DateTime.Now.Year;
            
            List<Stew> expiredStews = _stews.Where(stew => (stew.ProductionYear + stew.ShelfLife) < currentYear).ToList();
            
            ShowStewsInfo(expiredStews);
        }
        
        private void AddRandomStews(List<Stew> stews)
        {
            Random random = new Random();

            int minProductionYear = 2015;
            int maxProductionYear = 2023;
            int minShelfLife = 2;
            int maxShelfLife = 10;

            List<string> names = new List<string>
            {
                "тушенка1",
                "тушенка2",
                "тушенка3",
                "тушенка4",
                "тушенка5",
                "тушенка6",
                "тушенка7",
                "тушенка8",
                "тушенка9",
                "тушенка10"
            };

            for (int i = 0; i < names.Count; i++)
            {
                stews.Add(new Stew(names[i], random.Next(minProductionYear, maxProductionYear),
                    random.Next(minShelfLife, maxShelfLife)));
            }
        }

        private void ShowStewsInfo(List<Stew> stews)
        {
            foreach (Stew stew in stews)
            {
                Console.WriteLine($"Название - {stew.Name} Год производства - {stew.ProductionYear} " +
                                  $"Срок годности - {stew.ShelfLife}");
            }
        }
    }
}
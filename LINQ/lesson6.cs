using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Barracks barrack = new Barracks();
            
            barrack.ShowSoldiers();
        }

        class Soldier
        {
            public string Name { get; private set; }
            public string Armament { get; private set; }
            public string Rank { get; private set; }
            public int ServiceLife { get; private set; }

            public Soldier(string name, string armament, string rank, int serviceLife)
            {
                Name = name;
                Armament = armament;
                Rank = rank;
                ServiceLife = serviceLife;
            }
        }

        class Barracks
        {
            private List<Soldier> _soldiers;

            public Barracks()
            {
                _soldiers = new List<Soldier>();
                AddRandomSoldiers(_soldiers);
            }

            public void ShowSoldiers()
            {
                var soldiers = _soldiers.Select(soldier => new
                {
                    name = soldier.Name,
                    rank = soldier.Rank
                });
                    
                foreach (var soldier in soldiers)
                {
                    Console.WriteLine($"Имя - {soldier.name} Звание - {soldier.rank}");
                }
            }
            
            private void AddRandomSoldiers(List<Soldier> soldiers)
            {
                Random random = new Random();

                List<string> names = new List<string>
                {
                    "name1",
                    "name2",
                    "name3",
                    "name4",
                    "name5",
                    "name6",
                    "name7",
                    "name8",
                    "name9",
                    "name10"
                };
                
                List<string> armaments = new List<string>
                {
                    "armament1",
                    "armament2",
                    "armament3",
                    "armament4",
                    "armament5",
                };
                
                List<string> ranks = new List<string>
                {
                    "rank1",
                    "rank2",
                    "rank3",
                    "rank4",
                    "rank5"
                };

                int minServiceLife = 12;
                int maxServiceLife = 24;
                
                for (int i = 0; i < names.Count; i++)
                {
                    soldiers.Add(new Soldier(names[i], armaments[random.Next(armaments.Count)],
                        ranks[random.Next(ranks.Count)], random.Next(minServiceLife, maxServiceLife)));
                }
            }
        }
    }
}
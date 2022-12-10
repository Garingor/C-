using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Arena arena = new Arena();
            
            arena.Battle();
        }
    }

    class Arena
    {
        private Platoon _firstPlatoon;
        private Platoon _secondPlatoon;
        
        public Arena()
        {
            _firstPlatoon = new Platoon();
            _secondPlatoon = new Platoon();
        }

        public void Battle()
        {
            while (_firstPlatoon.GetCountColdiers() > 0 && _secondPlatoon.GetCountColdiers() > 0)
            {
                _firstPlatoon.Attack(_secondPlatoon);
                
                _secondPlatoon.Attack(_firstPlatoon);
            }
            
            PickWinner();
        }
        
        private void PickWinner()
        {
            if (_firstPlatoon.GetCountColdiers() > 0)
            {
                Console.WriteLine($"Победил первый взвод, солдат осталось - {_firstPlatoon.GetCountColdiers()}");
            }
            else if (_secondPlatoon.GetCountColdiers() > 0)
            {
                Console.WriteLine($"Победил второй взвод, солдат осталось - {_secondPlatoon.GetCountColdiers()}");
            }
            else
            {
                Console.WriteLine("Ничья");
            }
        }
    }
    
    class Platoon
    {
        private List<Soldier> _soldiers;

        public Platoon()
        {
            _soldiers = new List<Soldier>();
            AddRandomSoldiers();
        }

        public int GetCountColdiers()
        {
            return _soldiers.Count;
        }

        public void Attack(Platoon enemyPlatoon)
        {
            if (_soldiers.Count > 0 && enemyPlatoon.GetCountColdiers() > 0)
            {
                Random random = new Random();
                int idSoldier = random.Next(0, _soldiers.Count);
                int idEnemySoldier = random.Next(0, enemyPlatoon.GetCountColdiers());

                enemyPlatoon.TakeDamage(idEnemySoldier, _soldiers[idSoldier].Damage);
                
                enemyPlatoon.removeDeadSoldier(idEnemySoldier);
            }
        }

        private void TakeDamage(int idSoldier, int damage)
        {
            _soldiers[idSoldier].TakeDamage(damage);
        }

        private void removeDeadSoldier(int idSoldier)
        {
            if (_soldiers[idSoldier].Health <= 0)
            {
                RemoveSoldier(idSoldier);
            }
        }
        
        private void RemoveSoldier(int idEnemySoldier)
        {
           _soldiers.RemoveAt(idEnemySoldier);
        }
        
        private void AddRandomSoldiers()
        {
            int minCountSoldiers = 3;
            int maxCountSoldiers = 10;
            int minHealth = 100;
            int maxHealth = 150;
            int minDamage = 20;
            int maxDamage = 50;
            Random random = new Random();
            int countSoldiers = random.Next(minCountSoldiers, maxCountSoldiers);
            
            for (int i = 0; i < countSoldiers; i++)
            {
                _soldiers.Add(new Soldier(random.Next(minHealth, maxHealth), 
                    random.Next(minDamage, maxDamage)));
            }
        }
    }

    class Soldier
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }

        public Soldier(int health, int damage)
        {
            Health = health;
            Damage = damage;
        }
        
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}
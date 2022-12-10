using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Aquarium aquarium = new Aquarium();
            
            aquarium.Work();
        }
    }

    class Aquarium
    {
        private const string CommandAddFish = "1";
        private const string CommandDeleteFish = "2";
        private const string CommandExit = "0";
        
        private List<Fish> _fishes;
        private int _maxFishes;
        
        public Aquarium()
        {
            _maxFishes = 10;
            _fishes = new List<Fish>(_maxFishes);
            AddRandomFishes();
        }
    
        public void Work()
        {
            bool isOpen = true;
            
            while (isOpen)
            {
                ShowLog();
                
                AgeFishes();
                
                RemoveDeadFishes();
                
                Console.Write($"\n{CommandAddFish} - добавить рыбу в аквариум" + 
                              $"\n{CommandDeleteFish} - достать рыбу в аквариума" +
                              $"\n{CommandExit} - выход из программы" +
                              "\nДля продолжения нажмите любую клавишу" + 
                              "\nВведите команду: ");

                string choose = Console.ReadLine();
                
                switch (choose)
                {
                    case CommandAddFish:
                        AddFish();
                        break;
                    case CommandDeleteFish:
                        DeleteFish();
                        break;
                    case CommandExit:
                        isOpen = false;
                        break;
                }
            }
        }

        private void AgeFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].Health != 1)
                {
                    _fishes[i].ReduceHealth();
                }
            }
        }
        
        private void RemoveDeadFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].Health == 1)
                {
                    _fishes.RemoveAt(i);
                    i--;
                }
            }
        }
        
        private void AddRandomFishes()
        {
            int minHealth = 5;
            int maxHealth = 11;
            Random random = new Random();
            List<string> nameFishes = new List<string>() {"лещ", "налим", "карп", "сом", "кета", "барракуда"};

            for (int i = 0; i < nameFishes.Count; i++)
            {
                _fishes.Add(new Fish(nameFishes[i], random.Next(minHealth, maxHealth)));
            }
        }
        
        private void AddFish()
        {
            if (_fishes.Count < _maxFishes)
            {
                Console.Write("\nвведите название рыбы: ");
            
                string name = Console.ReadLine();
                
                Console.Write("\nвведите количество жизней у рыбы: ");
                
                if (int.TryParse(Console.ReadLine(), out int health) && health > 1)
                {
                    _fishes.Add(new Fish(name, health));
                }
                else
                {
                    Console.WriteLine("Количество жизней у рыбы не удалось корректно прочитать");
                }
            }
            else
            {
                Console.WriteLine("Аквариум переполнен, невозможно добавить новую рыбу");
            }
        }

        private void DeleteFish()
        {
            Console.Write("\nвведите номер рыбы: ");

            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < _fishes.Count)
            {
                _fishes.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("\nне удалось найти и удалить рыбу");
            }
        }
        
        private void ShowLog()
        {
            int index = 0;
            
            foreach (Fish fish in _fishes)
            {
                string bar = "";
                
                for (int i = 0; i < fish.Health; i++)
                {
                    bar += '#';
                }
                
                for (int i = 0; i < fish.MaxHealth - fish.Health; i++)
                {
                    bar += '_';
                }
                
                Console.Write("№"+ index + " " + fish.Name + " - [" + bar + "]\n");
                
                index++;
            }
        }
    }

    class Fish
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        
        public Fish(string name, int health)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
        }

        public void ReduceHealth()
        {
            if (Health > 0)
            {
                Health--;
            }
        }
    }
}
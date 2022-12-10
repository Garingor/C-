using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Nursery nursery = new Nursery();
            
            nursery.Start();
        }
    }

    class Nursery //питомник
    {
        private List<Aviary> _aviaries;

        public Nursery()
        {
            _aviaries = new List<Aviary>();
            AddRandomAviaries();
        }

        public void Start()
        {
            const ConsoleKey CommandExit = ConsoleKey.Enter;
            
            bool isOpen = true;

            while (isOpen)
            {
                ShowAviaries();
                
                Console.Write("К какому вольеру подойти: ");
            
                if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < _aviaries.Count)
                {
                    _aviaries[index].ShowAnimals();
                }
                
                Console.Write("Для выхода из программы нажмите Enter/для продолжения любую клавишу\n");
                
                if (Console.ReadKey(true).Key == CommandExit)
                {
                    isOpen = false;
                }
            }
        }

        private void ShowAviaries()
        {
            for (int i = 0; i < _aviaries.Count; i++)
            {
                Console.WriteLine($"№{i} вольер");
            }
        }

        private void AddRandomAviaries()
        {
            Dictionary<string, string> namesAndSounds = new Dictionary<string, string>() { {"собак", "лай"}, 
                {"котов", "мяуканье"}, {"коней", "фырканье"}, {"птиц", "пение" } };

            foreach (KeyValuePair<string, string> nameAndSound in namesAndSounds)
            {
                _aviaries.Add(new Aviary(nameAndSound.Key, nameAndSound.Value));
            }
        }
    }
    
    class Animal
    {
        public string Sex { get; private set; }

        public Animal(string sex)
        {
            Sex = sex;
        }
    }

    class Aviary //вольер
    {
        private List<Animal> _animals;
        private string _name;
        private string _sound;
        
        public Aviary(string name, string sound)
        {
            _animals = new List<Animal>();
            _name = name;
            _sound = sound;
            AddRandomAnimals();
        }

        private void AddRandomAnimals()
        {
            Random random = new Random();
            int minCountAnimals = 2;
            int maxCountAnimals = 10;
            List<string> sex = new List<string>() { "М", "Ж" };

            int countAnimals = random.Next(minCountAnimals,maxCountAnimals);

            for (int i = 0; i < countAnimals; i++)
            {
                _animals.Add(new Animal(sex[random.Next(sex.Count)]));
            }
        }
        
        public void ShowAnimals()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                Console.WriteLine($"№{i} пол - {_animals[i].Sex}");
            }
            
            Console.WriteLine($"Вольер для {_name}");
            
            Console.WriteLine($"В вольере звук - {_sound}");
            
            Console.WriteLine($"В вольере находится {_animals.Count} животных");
        }
    }
}
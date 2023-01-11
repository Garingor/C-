using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Game game = new Game();
            
            game.ShowTopPlayers();
        }
    }

    class Player
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Force { get; private set; }

        public Player(string name, int level, int force)
        {
            Name = name;
            Level = level;
            Force = force;
        }
    }

    class Game
    {
        private List<Player> _players;

        public Game()
        {
            _players = new List<Player>();
            AddRandomPlayers(_players);
        }

        public void ShowTopPlayers()
        {
            ShowTopPlayerByLevel();
            
            ShowTopPlayerByForce();
        }

        private void ShowTopPlayerByLevel()
        {
            string message = "Топ 3 игрока по уровню";
            
            List<Player> filteredPlayers = _players.OrderByDescending(player => player.Level).Take(3).ToList();
            
            ShowPlayers(filteredPlayers, message);
        }
        
        private void ShowTopPlayerByForce()
        {
            string message = "Топ 3 игрока по cиле";
            
            List<Player> filteredPlayers = _players.OrderByDescending(player => player.Force).Take(3).ToList();
            
            ShowPlayers(filteredPlayers, message);
        }
        
        private void AddRandomPlayers(List<Player> players)
        {
            Random random = new Random();

            int minLevel = 1;
            int maxLevel = 10;
            int minForce = 10;
            int maxFoce = 100;

            List<string> names = new List<string>
            {
                "qwerty1",
                "qwerty2",
                "qwerty3",
                "qwerty4",
                "qwerty5",
                "qwerty6",
                "qwerty7",
                "qwerty8",
                "qwerty9",
                "qwerty10"
            };

            for (int i = 0; i < names.Count; i++)
            {
                players.Add(new Player(names[i], random.Next(minLevel, maxLevel), 
                    random.Next(minForce, maxFoce)));
            }
        }

        private void ShowPlayers(List<Player> players, string message)
        {
            Console.WriteLine(message + ":");
            
            foreach (Player player in players)
            {
                Console.WriteLine($"Имя - {player.Name} Уровень - {player.Level} Сила - {player.Force}");
            }
        }
    }
}
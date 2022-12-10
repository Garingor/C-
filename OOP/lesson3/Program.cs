using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddPlayer = "1";
            const string CommandBanPlayer = "2";
            const string CommandUnbanPlayer = "3";
            const string CommandDeletePlayer = "4";
            const string CommandExit = "0";
            
            Database players = new Database();
            bool isExit = false;

            while (isExit == false)
            {
                Console.Write("\n");
                Console.Write($"\n{CommandAddPlayer} - добавить игрока" +
                              $"\n{CommandBanPlayer} - забанить игрока" +
                              $"\n{CommandUnbanPlayer} - разбанить игрока" +
                              $"\n{CommandDeletePlayer} - удалить игрока" +
                              $"\n{CommandExit} - выйти из программы" +
                              "\nВведите команду: ");
                
                string choose = Console.ReadLine();
                
                switch (choose)
                {
                    case CommandAddPlayer:
                        players.AddPlayer();
                        break;
                    case CommandBanPlayer:
                        players.BanPlayer();
                        break;
                    case CommandUnbanPlayer:
                        players.UnbanPlayer();
                        break;
                    case CommandDeletePlayer:
                        players.DeletePlayer();
                        break;
                    case CommandExit:
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("Команда не распознана");
                        break;
                }
            }
        }
    }

    class Player
    {
        private int _id;
        private string _nickname;
        private int _level;
        private bool _ban;
        
        public int Id 
        {
            get
            {
                return _id;
            }
        }
        
        public Player(int id, string nickname, int level, bool isbanned = false)
        {
            _id = id;
            _nickname = nickname;
            _level = level;
            _ban = isbanned;
        }
        
        public void Show()
        {
            Console.WriteLine($"id - {_id} никнейм - {_nickname} уровень - {_level} Забанен - " +
                              $"{_ban}");
        }
        
        public void Unban()
        {
            _ban = false;
        }

        public void Ban()
        {
            _ban = true;
        }
    }
    
    class Database
    {
        private int _lastId;
        private List<Player> _players;
        
        public Database()
        {
            _lastId = 0;
            _players = new List<Player>();
        }
        
        public void ShowPlayers()
        {
            foreach (Player player in _players)
            {
                player.Show();
            }
        }
        
        public void AddPlayer()
        {
            Console.Write("Введите никнейм игрока: ");
            string nickname = Console.ReadLine();
            Console.Write("Введите уровень игрока: ");
            
            if (int.TryParse(Console.ReadLine(), out int level))
            {
                _players.Add(new Player(_lastId, nickname, level));
                _lastId++;
            
                Console.WriteLine($"Игрок с никнеймом - {nickname} успешно добавлен");
            }
        }

        public void BanPlayer()
        {
            Player player;
            
            if (TryGetPlayer(out player))
            {
                player.Ban();
                Console.WriteLine($"игрок с id - {player.Id} забанен");
            }
            else
            {
                Console.WriteLine("BanPlayer() исключение - невалидный id");
            }
        }

        public void UnbanPlayer()
        {
            Player player;
            
            if (TryGetPlayer(out player))
            {
                player.Unban();
                Console.WriteLine($"игрок с id - {player.Id} разбанен");
            }
            else
            {
                Console.WriteLine("UnBanPlayer() исключение - невалидный id");
            }
        }

        public void DeletePlayer()
        {
            Player player;
            
            if (TryGetPlayer(out player))
            {
                _players.Remove(player);
                Console.WriteLine($"игрок с id - {player.Id} удален");
            }
            else
            {
                Console.WriteLine("UnBanPlayer() исключение - невалидный id");
            }
        }
        
        private bool TryGetPlayer(out Player player)
        {
            bool isExists = false;
            player = null;
            
            Console.Write("Введите id игрока: ");
            
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                foreach (Player item in _players)
                {
                    if (item.Id == id)
                    {
                        isExists = true;
                        player = item;
                        break;
                    }
                }
            }
            else
            {
                player = null;
                Console.WriteLine("игрок не найден");
            }

            return isExists;
        }
    }
}
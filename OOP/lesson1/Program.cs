using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new Player();
            Player player2 = new Player();
            
            player1.ShowInfo();
            player2.ShowInfo();
        }

        class Player
        {
            private int _health;
            private int _damage;
            private int _speed;
            private int _armor;
            
            public Player()
            {
                _health = 100;
                _damage = 10;
                _speed = 5;
                _armor = 15;
            }

            public Player(int health, int damage, int speed, int armor)
            {
                _health = health;
                _damage = damage;
                _speed = speed;
                _armor = armor;
            }

            public void ShowInfo()
            {
                Console.WriteLine($"\nЗдоровье - {_health}\nУрон - {_damage}\nСкорость - {_speed}\nБроня - {_armor}");
            }
        }
    }
}
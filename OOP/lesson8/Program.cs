using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            Arena arena = new Arena();
            
            arena.PlayBattle();
        }
    }

    class Arena
    {
        private const string CommandFight = "1";
        private const string CommandExit = "0";

        private Fighter _leftFighters;
        private Fighter _rightFighters;

        public Arena()
        {
            _leftFighters = null;
            _rightFighters = null;
        }

        public void PlayBattle()
        {
            bool isOpen = true;
            
            while (isOpen)
            {
                Console.Write($"\n{CommandFight} - выберите бойцов" +
                              $"\n{CommandExit} - выход из программы" +
                              "\nВыберите команду ");

                string choose = Console.ReadLine();

                switch (choose)
                {
                    case CommandFight:
                        Fight();
                        break;
                    case CommandExit:
                        isOpen = false;
                        break;
                    default:
                        Console.WriteLine("команда не распознана, попробуйте еще раз");
                        break;
                }
            }   
        }

        private void Fight()
        {
            _leftFighters = SelectFighter();
                    
            _rightFighters = SelectFighter();

            Console.WriteLine("\nБой");
            
            while (_leftFighters.Health > 0 && _rightFighters.Health > 0)
            { 
                _leftFighters.Attack(_rightFighters);
                _rightFighters.ShowLog(2);

                _rightFighters.Attack(_leftFighters);
                _leftFighters.ShowLog(1);
            }

            PickWinner();

            RestoreFighters();
        }

        private void PickWinner()
        {
            if (_leftFighters.Health > 0)
            {
                Console.WriteLine($"Победил первый игрок - {_leftFighters.Name}");
            }
            else if (_rightFighters.Health > 0)
            {
                Console.WriteLine($"Победил второй игрок - {_rightFighters.Name}");
            }
            else
            {
                Console.WriteLine("Ничья");
            }
        }
        
        private Fighter SelectFighter()
        {
            List<Fighter> selectionFighters = new List<Fighter>()
                { new Warrior(), new Rogue(), new Mage(), new Paladin(), new Cleric() };
            bool isOpen = true;
            int id = 0;
            
            for (int i = 0; i < selectionFighters.Count; i++)
            {
                Console.WriteLine($"\n{selectionFighters[i].ShowStats(i)}");
            }
            
            Console.Write("\nВыберите героя: ");
            
            while (isOpen)
            {
                if (int.TryParse(Console.ReadLine(), out id) && id >= 0 && id < selectionFighters.Count)
                {
                    isOpen = false;
                }
                else
                {
                    Console.WriteLine("команда не распознана, попробуйте еще раз");
                }
            }
            
            return selectionFighters[id];
        }
        
        private void RestoreFighters()
        {
            _leftFighters = null;
            _rightFighters = null;
        }
    }

    abstract class Fighter
    {
        public string Name{ get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }

        public abstract string ShowStats(int id);
        
        public abstract void ShowLog(int id);
        
        public abstract void Attack(Fighter enemy);

        public virtual void TakeDamage(int damage)
        {
            if (damage - Armor > 0)
            {
                Health -= damage - Armor;
            }
        }
        
        public virtual void TakeNoBlockedDamage(int damage)
        {
            if (damage - Armor > 0)
            {
                Health -= damage;
            }
        }

        protected bool CanUse(int maxchance)
        {
            int minPercent = 1;
            int maxPercent = 101;
            
            Random random = new Random();
            int chance = random.Next(minPercent,maxPercent);

            if (chance <= maxchance)
            {
                return true;
            }

            return false;
        }
    }

    class Warrior : Fighter
    {
        private int _maxchance;
        private int _maxIncreaseDamage;
        
        public Warrior()
        {
            Name = "Воин";
            Health = 1000;
            Damage = 20;
            Armor = 10;
            _maxchance = 7;
            _maxIncreaseDamage = 2;
        }
        
        public override void ShowLog(int id)
        {
            Console.WriteLine($"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor}");
        }
        
        public override string ShowStats(int id)
        {
            return $"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor} " +
                   $"\nспособность - x{_maxIncreaseDamage} урон с шансом в {_maxchance}%";
        }
        
        public override void Attack(Fighter enemy)
        {
            if (CanUse(_maxchance))
            {
                enemy.TakeDamage(Damage * _maxIncreaseDamage);
            }
            else
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
    
    class Rogue : Fighter
    {
        private int _maxchance;
        
        public Rogue()
        {
            Name = "Плут";
            Health = 1000;
            Damage = 20;
            Armor = 5;
            _maxchance = 15;
        }

        public override void ShowLog(int id)
        {
            Console.WriteLine($"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor}");
        }
        
        public override string ShowStats(int id)
        {
            return $"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor}" +
            $"\nспособность - противника бъет своим же уроном шансом в {_maxchance}%";
        }
        
        public override void Attack(Fighter enemy)
        {
            if (CanUse(_maxchance))
            {
                enemy.TakeDamage(enemy.Damage);
            }
            else
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
    
    class Mage : Fighter
    {
        private int _mana;
        private int _maxMana;
        private int _manaIncreasePerTurn;
        
        public Mage()
        {
            Name = "Маг";
            Health = 1000;
            Damage = 25;
            Armor = 6;
            _mana = 0;
            _maxMana = 40;
            _manaIncreasePerTurn = 10;
        }
        
        public override void ShowLog(int id)
        {
            Console.WriteLine($"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor} мана - {_mana}");
        }
        
        public override string ShowStats(int id)
        {
            return $"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor} мана - {_mana} (max 40)" +
                   $"\nспособность - после накопления маны до {_maxMana} (за одну атаку +{_manaIncreasePerTurn} маны) " +
                   $"и кастования способности, щит противника пропускает весь урон";
        }
        
        public override void Attack(Fighter enemy)
        {
            if (_mana >= _maxMana)
            {
                enemy.TakeNoBlockedDamage(Damage);
                _mana = 0;
            }
            
            _mana += _manaIncreasePerTurn;
        }
    }
    
    class Paladin : Fighter
    {
        private int _maxchance;
        private int _countAttack;
        
        public Paladin()
        {
            Name = "Паладин";
            Health = 1000;
            Damage = 40;
            Armor = 0;
            _maxchance = 15;
            _countAttack = 2;
        }

        public override void ShowLog(int id)
        {
            Console.WriteLine($"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor}");
        }
        
        public override string ShowStats(int id)
        {
            return $"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor}" +
                   $"\nспособность - {_countAttack} атаки подряд c шансом  {_maxchance}% ";
        }
        
        public override void Attack(Fighter enemy)
        {
            if (CanUse(_maxchance))
            {
                for (int i = 0; i < _countAttack; i++)
                {
                    enemy.TakeDamage(Damage);
                }
            }
            else
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
    
    class Cleric : Fighter
    {
        private int _castCounter;
        private int _maxCastCounter;
        private bool _castFlag;
        private int _defaultDamage;
        private int _defaultArmor;
        private int _maxchance;
        private int _maxIncreaseArmor;
        private int _maxDecreaseDamage;
        
        public Cleric()
        {
            Name = "Жрец";
            Health = 1200;
            Damage = 15;
            _defaultDamage = Damage;
            Armor = 3;
            _defaultArmor = Armor;
            _castCounter = 0;
            _maxCastCounter = 2;
            _castFlag = false;
            _maxchance = 10;
            _maxIncreaseArmor = 4;
            _maxDecreaseDamage = 0;
        }
    
        public override void ShowLog(int id)
        {
            Console.WriteLine($"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor}");
        }
        
        public override string ShowStats(int id)
        {
            return $"№{id} {Name}: здоровье - {Health} урон - {Damage} броня - {Armor}" +
                   $"\nспособность - увеличение брони в {_maxIncreaseArmor} раза и урон равен {_maxDecreaseDamage} " +
                   $"на {_castCounter} хода c шансом {_maxchance}% ";
        }
        
        public override void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);
        }
        
        public override void TakeDamage(int damage)
        {
            if (CanUse(_maxchance) && _castFlag == false)
            {
                Armor *= _maxIncreaseArmor;
                Damage = _maxDecreaseDamage;
                _castFlag = true;
            }

            if (_castFlag)
            {
                _castCounter++;
            }

            if (_castCounter == _maxCastCounter)
            {
                Damage = _defaultDamage;
                Armor = _defaultArmor;
                _castCounter = 0;
                _castFlag = false;
            }
            
            if (damage - Armor > 0)
            {
                Health -= damage - Armor;
            }
        }
        
        public override void TakeNoBlockedDamage(int damage)
        {
            if (CanUse(_maxchance) && _castFlag == false)
            {
                Armor *= _maxIncreaseArmor;
                Damage = _maxDecreaseDamage;
                _castFlag = true;
            }

            if (_castFlag)
            {
                _castCounter++;
            }

            if (_castCounter == _maxCastCounter)
            {
                Damage = _defaultDamage;
                Armor = _defaultArmor;
                _castCounter = 0;
                _castFlag = false;
            }
            
            if (damage - Armor > 0)
            {
                Health -= damage;
            }
        }
    }
}
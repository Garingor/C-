using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            int sellerMoney = 100;
            int playerMoney = 100;
            
            Market market = new Market(sellerMoney, playerMoney);

            market.Work();
        }
    }

    class Market
    {
        private const string CommandShowCellsSeller = "1";
        private const string CommandShowCellsPlayer = "2";
        private const string CommandSellCell = "3";
        private const string CommandExit = "0";
        
        private Seller _seller;
        private Player _player;

        public Market(int sellerMoney, int playerMoney)
        {
            _seller = new Seller(sellerMoney);
            _player = new Player(playerMoney);
        }
        
        public void Work()
        {
            bool isExit = false;
            
            while (isExit == false)
            {
                Console.Write($"\n{CommandShowCellsSeller} - показать доступные продукты у продавца" +
                              $"\n{CommandShowCellsPlayer} - показать доступные продукты у игрока" +
                              $"\n{CommandSellCell} - продать продукт" +
                              $"\n{CommandExit} - выход из программы" +
                              "\nВыберите команду ");

                string choose = Console.ReadLine();

                switch (choose)
                {
                    case CommandShowCellsSeller:
                        _seller.ShowCells();
                        break;
                    case CommandShowCellsPlayer:
                        _player.ShowCells();
                        break;
                    case CommandSellCell:
                        Trade();
                        break;
                    case CommandExit:
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine("команда не распознана, попробуйте еще раз");
                        break;
                }
            }
        }
        
        public void Trade()
        {
            Console.Write($"\nбаланс игрока - {_player.Money}");
            
            _seller.ShowCells();
            
            Console.Write("Под каким номером хотите купить товар: ");
            
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < _seller.CellsCount)
            {
                if (_player.IsMoneyEnough(_seller.GetProductPrice(index)))
                {
                    _player.Purchase(_seller.GetProductName(index), _seller.GetProductPrice(index));
                    _seller.Sale(index);
                }
                else
                {
                    Console.WriteLine("недостаточно денег на счету");
                }
            }
            else
            {
                Console.WriteLine("Номер не распознан");
            }
        }
    }

    class Сharacter
    {
        protected List<Cell> Cells;
        public int CellsCount => Cells.Count;
        public int Money { get; protected set; }
        
        protected Сharacter(int money)
        {
            Money = money;
            Cells = new List<Cell>();
        }

        public int GetProductPrice(int index)
        {
            return Cells[index].Product.Price;
        }
        
        public string GetProductName(int index)
        {
            return Cells[index].Product.Name;
        }
        
        public bool IsMoneyEnough(int price)
        {
            return Money > price;
        }
        
        public virtual void ShowCells()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                Console.WriteLine($"№{i} название - {Cells[i].Product.Name} цена - {Cells[i].Product.Price} " +
                                  $"количество - {Cells[i].Count}");
            }
        }
    }
    
    sealed class Seller : Сharacter
    {
        public Seller(int money = 0) : base(money)
        {
            AddCells();
        }

        public override void ShowCells()
        {
            Console.WriteLine($"\nбаланс продавца - {Money}");
            base.ShowCells();
        }

        public void Sale(int index)
        {
            if (index >= 0 && index < Cells.Count)
            {
                Cells[index].DecrementCountProduct();

                if (Cells[index].Count == 0)
                {
                    Cells.Remove(Cells[index]);
                }
            }
            
            Money += Cells[index].Product.Price;
        }

        private void AddCells()
        {
            int minPrice = 10;
            int maxPrice = 50;
            int minCountProducts = 1;
            int maxCountProducts = 4;
            
            List<string> items = new List<string>() {"броня (+15)", "лечение (+10)", "повышение урона (+50)"};
            Random random = new Random();
            int count;
            Cell newCell;

            for (int i = 0; i < items.Count; i++)
            {
                count = random.Next(minCountProducts, maxCountProducts);
                
                newCell = new Cell(items[i], random.Next(minPrice, maxPrice), count);
                
                Cells.Add(newCell);
            }
        }
    }

    sealed class Player : Сharacter
    {
        public Player(int money = 0) : base(money) { }

        public override void ShowCells()
        {
            Console.WriteLine($"\nбаланс покупателя - {Money}");
            base.ShowCells();
        }
        
        public void Purchase(string name, int price)
        {
            bool isFound = false;
            
            foreach (Cell cell in Cells)
            {
                if (cell.Product.Name == name && cell.Product.Price == price)
                {
                    cell.IncrementCount();
                    isFound = true;
                    break;
                }
            }

            if (isFound == false)
            {
                Cells.Add(new Cell(name, price, 1));
            }

            Money -= price;
        }
    }

    class Cell
    {
        public Product Product { get; private set; }
        public int Count { get; private set; }
    
        public Cell(string name, int price, int count)
        {
            Product = new Product(name, price);
            Count = count;
        }

        public void DecrementCountProduct()
        {
            if (Count > 0)
            {
                Count--;
            }
        }
        
        public void IncrementCount()
        {
            if (Count < Int32.MaxValue)
            {
                Count++;
            }
        }
    }
    
    class Product
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }
}
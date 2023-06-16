using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            CarService carService = new CarService();

            carService.Work();
        }
    }

    class CarService
    {
        private int _money;
        private Storage _storage;
        private List<Car> _brokeCars;
        private int _payPerHour;
        private int _penalty;

        public CarService()
        {
            _storage = new Storage();
            _brokeCars = new List<Car>();
            _payPerHour = 20;
            _penalty = 50;

            AddRandomMoney();
            AddRandomBrokeCars();
        }

        public void Work()
        {
            int totalCost;
            int indexCarForService = 0;
            
            //for(int idCurrentCarForService = _brokeCars.Count - 1; idCurrentCarForService >= 0; idCurrentCarForService--)
            while(IsQueueNotEmpty())
            {
                ShowBrokeCar(indexCarForService);
                
                totalCost = FixCar(_brokeCars[indexCarForService]);

                Console.WriteLine($"Автосервис заработал с починки машины - {totalCost}");
                
                _money += totalCost;

                if (_money < 0)
                {
                    Console.WriteLine("||Автосервис банкрот и закрывается||");
                    ClearQueue();                 
                }

                Console.WriteLine($"Текущий счет автосервиса - {_money}\n");
            }

            Console.WriteLine($"\nИтоговый счет автосервиса - {_money}");
        }

        private bool IsQueueNotEmpty()
        {
            return _brokeCars.Count > 0;
        }

        private void ClearQueue()
        {
            _brokeCars.Clear();
        }

        private int FixCar(Car brokeCar)
        {
            int money = 0;
            int workPrice;
            int costDetail;
            List<string> brokeDetails = brokeCar.GetCopyBrokeDetails();
            Cell storageCell;

            foreach (string brokeDetail in brokeDetails)
            {
                storageCell = _storage.GetDetail(brokeDetail);
                
                if(storageCell == null)
                {    
                    Console.WriteLine($"Детали нет на складе, автосервис " +
                                          $"выплатит штраф в размере {_penalty}");
                    money -= _penalty;
                }
                else
                {
                    if (brokeDetail == storageCell.Detail.Name)
                    {
                        costDetail = GetDetailPrice(storageCell.Detail.Name);
                        money += costDetail;
                        workPrice = GetWorkPrice(storageCell.Detail.Name);
                        money += workPrice;

                        Console.WriteLine($"Заменили деталь - {storageCell.Detail.Name}, " +
                                        $"Стоимость детали - {costDetail} " + 
                                        $"Стоимость работы по установке детали - {workPrice}");
                    }
                    else
                    {
                        costDetail = GetDetailPrice(storageCell.Detail.Name);
                        money -= costDetail;
                        
                        Console.WriteLine($"Заменили не ту деталь - {storageCell.Detail.Name}, " +
                                        $"клиенту возмещено за эту деталь - {costDetail}");
                    }

                    if(storageCell.Quantity == 0)
                    {
                        _storage.RemoveCellByName(storageCell.Detail.Name);
                    }
                }
            }

            return money;
        }

        private int GetWorkPrice(string name)
        {
            int timeForHardWork = 3;
            int timeForMediumWork = 2;
            int timeForEasyWork = 1;

            Dictionary<string, int> workPrice = new Dictionary<string, int>()
            {
                {"Дверная ручка", _payPerHour * timeForMediumWork},
                {"Датчик давления в шине", _payPerHour * timeForEasyWork},
                {"Тормозной диск", _payPerHour * timeForEasyWork},
                {"Передняя фара", _payPerHour * timeForMediumWork},
                {"Задний габарит",_payPerHour * timeForEasyWork},
                {"Аккумулятор", _payPerHour * timeForHardWork}, 
                {"Лобовое стекло", _payPerHour * timeForHardWork}, 
                {"Заднее стекло", _payPerHour * timeForHardWork}
            };

            return workPrice[name];
        }

        private int GetDetailPrice(string name)
        {
            int priceCheapDetail = 100;
            int priceExpensiveDetail = 200;

            Dictionary<string, int> detailPrice = new Dictionary<string, int>()
            {
                {"Дверная ручка", priceCheapDetail},
                {"Датчик давления в шине", priceCheapDetail},
                {"Тормозной диск", priceExpensiveDetail},
                {"Передняя фара", priceExpensiveDetail},
                {"Задний габарит", priceExpensiveDetail},
                {"Аккумулятор", priceCheapDetail}, 
                {"Лобовое стекло", priceExpensiveDetail}, 
                {"Заднее стекло",priceExpensiveDetail}
            };
            
            return detailPrice[name];
        }

        private void ShowBrokeCar(int idCurrentCarForService)
        {
            _brokeCars[idCurrentCarForService].ShowBrokeDetails();
        }

        private void AddRandomMoney()
        {
            Random random = new Random();
            int minMoney = 500;
            int maxMoney = 5000;

            _money = random.Next(minMoney, maxMoney);
        }

        private void AddRandomBrokeCars()
        {
            Random random = new Random();
            int minCountBrokeCars = 1;
            int maxCountBrokeCars = 3;
            int countBrokeCars = random.Next(minCountBrokeCars, maxCountBrokeCars);
            
            for (int i = 0; i < countBrokeCars; i++)
            {
                _brokeCars.Add(new Car());
            }
        }
    }

    class Detail
    {
        public Detail(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
    
    class Cell
    {
        public Cell(string name, int quantity)
        {
            Detail = new Detail(name);
            Quantity = quantity;
        }
        
        public Detail Detail { get; private set; }
        public int Quantity { get; private set; }

        public void DecrementQuantity()
        {
            Quantity--;
        }
    }

    class Storage
    {
        private List<string> _detailsNames;
        private List<Cell> _cells;

        public Storage()
        {
            _detailsNames = new List<string>()
            {
                "Дверная ручка",
                "Датчик давления в шине",
                "Тормозной диск",
                "Передняя фара",
                "Задний габарит",
                "Аккумулятор",
                "Лобовое стекло",
                "Заднее стекло"
            };

            _cells = new List<Cell>();

            AddRandomCells();
        }

        public void RemoveCellByName(string name)
        {
            foreach(Cell cell in _cells)
            {
                if(cell.Detail.Name == name)
                {
                    _cells.Remove(cell);
                }
            }
        }

        public Cell GetDetail(string desiredNameDetail)
        {
            if(_cells == null)
            {
                return null;
            }

            Random random = new Random();
            int minPercent = 1;
            int maxPercent = 101;
            int maxchance = 10;
            int index;

            int chance = random.Next(minPercent,maxPercent);

            if (chance <= maxchance)
            {
                List<Cell> copyCells = GetCopyCells();
                Cell copyCell = CopyCellByName(desiredNameDetail, _cells);
                copyCells = RemoveCellByName(desiredNameDetail, copyCells);

                index = random.Next(copyCells.Count);

                _cells[index].DecrementQuantity();
                
                _cells.Add(new Cell(copyCell.Detail.Name, copyCell.Quantity));
                
                return new Cell(_cells[index].Detail.Name, _cells[index].Quantity);
            }
            else
            {
                foreach (Cell cell in _cells)
                {
                    if(desiredNameDetail == cell.Detail.Name)
                    {
                        cell.DecrementQuantity();
                        
                        return new Cell(cell.Detail.Name, cell.Quantity);
                    }
                }
            }

            return null;
        }

        public void RemoveCell(Cell cell)
        {
            _cells.Remove(cell);
        }

        private List<Cell> RemoveCellByName(string name, List<Cell> cells)
        {
            foreach(Cell cell in cells)
            {
                if(cell.Detail.Name == name)
                {
                    cells.Remove(cell);

                    return cells;
                }
            }

            return null;
        }

        private Cell CopyCellByName(string name, List<Cell> cells)
        {
            foreach(Cell cell in cells)
            {
                if(cell.Detail.Name == name)
                {
                    return new Cell(cell.Detail.Name, cell.Quantity);
                }
            }

            return null;
        }

        private void AddRandomCells()
        {
            Random random = new Random();
            int maxQuantity = 15;

            for (int i = 0; i < _detailsNames.Count; i++)
            {
                _cells.Add(new Cell(_detailsNames[i], random.Next(maxQuantity)));
            }
        }

        private List<Cell> GetCopyCells()
        {
            List<Cell> cells = new List<Cell>();

            foreach (var cell in _cells)
            {
                cells.Add(new Cell(cell.Detail.Name, cell.Quantity));
            }

            return cells;
        }
    }

    class Car
    {
        private List<string> _brokeDetails;
        private int _maxBrokeHandles;
        private int _maxBrokePressureMeters;
        private int _maxBrokeBrakeDisks;
        private int _maxBrokeHeadlights;
        private int _maxBrokeLights;
        private int _maxBrokeBatteries;
        private int _maxBrokeWindShields;
        private int _maxBrokeRearWindshields;
        
        public Car()
        {
            _maxBrokeHandles = 4;
            _maxBrokePressureMeters = 4;
            _maxBrokeBrakeDisks = 4;
            _maxBrokeHeadlights = 2;
            _maxBrokeLights = 2;
            _maxBrokeBatteries = 1;
            _maxBrokeWindShields = 1;
            _maxBrokeRearWindshields = 1;
            
            _brokeDetails = new List<string>();

            AddRandomBrokeDetails();
        }

        public List<string> GetCopyBrokeDetails()
        {
            List<string> brokeDetails = new List<string>();

            foreach (var brokeDetail in _brokeDetails)
            {
                brokeDetails.Add(brokeDetail);
            }

            return brokeDetails;
        }
        
        public void ShowBrokeDetails()
        {
            foreach (var brokeDetail in _brokeDetails)
            {
                Console.WriteLine($"поломка - {brokeDetail}");
            }
        }

        private void AddRandomBrokeDetails()
        {
            AddRandomCountDetails(_maxBrokeHandles, "Дверная ручка");
            AddRandomCountDetails(_maxBrokePressureMeters, "Датчик давления в шине");
            AddRandomCountDetails(_maxBrokeBrakeDisks, "Тормозной диск");
            AddRandomCountDetails(_maxBrokeHeadlights, "Передняя фара");
            AddRandomCountDetails(_maxBrokeLights, "Задний габарит");
            AddRandomCountDetails(_maxBrokeBatteries, "Аккумулятор");
            AddRandomCountDetails(_maxBrokeWindShields, "Лобовое стекло");
            AddRandomCountDetails(_maxBrokeRearWindshields, "Заднее стекло");
        }

        private void AddRandomCountDetails(int _maxBrokeDetails, string detail)
        {   
            Random random = new Random();

            int randomCountDetails = random.Next(_maxBrokeDetails);
            
            for (int i = 0; i < randomCountDetails; i++)
            {
                _brokeDetails.Add(detail);
            }
        }
    }
}

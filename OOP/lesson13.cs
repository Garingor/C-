using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            CarService carService = new CarService();
            
            carService.Service();
        }
    }

    class DetailsBase
    {
        private List<string> _detailsNames;
        private List<Cell> _cells;
        
        public DetailsBase(int handles, int pressureMeters, int brakeDisks, int headlights, 
            int lights, int batteries, int windshields, int rearWindshields)
        {
            _cells  = new List<Cell>
            {
                new Cell("Дверная ручка", handles),
                new Cell("Датчик давления в шине", pressureMeters),
                new Cell("Тормозной диск", brakeDisks),
                new Cell("Передняя фара", headlights),
                new Cell("Задний габарит", lights),
                new Cell("Аккумулятор", batteries),
                new Cell("Лобовое стекло", windshields),
                new Cell("Заднее стекло", rearWindshields)
            };
        }

        public DetailsBase()
        {
            _detailsNames  = new List<string>
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
        }

        public int GetCountDetailsNames()
        {
            return _detailsNames.Count;
        }
        
        public string GetDetailName(int i)
        {
            return _detailsNames[i];
        }
        
        public int GetCount(string name)
        {
            int errorCode = -1;
            
            foreach (Cell cell in _cells)
            {
                if (cell.Detail.Name == name)
                {
                    return cell.Count;
                }
            }

            return errorCode;
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

        public void Service()
        {
            int totalCost;
            int allEarnMoney = 0;
            int indexFirstCarForService = 0;
            
            while (IsQueueNotEmpty())
            {
                ShowBrokeCar();
                totalCost = ShowRepairCost();
                
                if (IsAvailableDetails(_brokeCars[indexFirstCarForService]))
                {
                    totalCost -= FixCar(_brokeCars[indexFirstCarForService]);
                    
                    _money += totalCost;
                    allEarnMoney += totalCost;

                    Console.WriteLine($"Автосервис заработал с починки машины - {totalCost}");
                }
                else
                {
                    Console.WriteLine($"Деталей не оказалось на складе, автосервис выплатит штраф в размере {_penalty}");

                    if (_money - _penalty > 0)
                    {
                        _money -= _penalty;
                    }
                    else
                    {
                        _money -= _penalty;
                        Console.WriteLine("||автосервис банкрот и закрывается||");
                        ClearQueue();
                    }
                }
            }
            
            Console.WriteLine($"автосервис всего заработал - {allEarnMoney}");
            Console.WriteLine($"на счету автосервиса - {_money}");
        }
        
        private bool IsQueueNotEmpty()
        {
            return _brokeCars.Count > 0;
        }
        
        private void ClearQueue()
        {
            _brokeCars.Clear();
        }
        
        private void ShowBrokeCar()
        {
            int idFirstCarForService = 0;
            
            _brokeCars[idFirstCarForService].ShowBrokeDetails();
        }
        
        private int ShowRepairCost()
        {
            int indexFirstCarForService = 0;
            int detailsCost = CalculateDetailsCost(indexFirstCarForService);
            int workCost = CalculateWorkCost(indexFirstCarForService);
            int totalCost = detailsCost + workCost;
            
            Console.WriteLine($"стоимость деталей без учета работы мастера - {detailsCost}");
            
            Console.WriteLine($"стоимость работы мастера - {workCost}");
            
            Console.WriteLine($"общая стоимость работы - {totalCost}");

            return totalCost;
        }

        private bool IsAvailableDetails(Car brokeCar)
        {
            List<Cell> brokeDetails = brokeCar.GetCopyBrokeDetails();

            return _storage.IsAvailableDetails(brokeDetails);
        }

        private int FixCar(Car brokeCar)
        {
            int money = 0;
            int costDetail;
            List<Cell> brokeDetails = brokeCar.GetCopyBrokeDetails();
            string nameDetail;

            while (brokeDetails.Count > 0)
            {
                foreach (Cell desiredDetail in brokeDetails.ToList())
                {
                    if (IsAvailableDetails(brokeCar))
                    {
                        nameDetail = _storage.GetDetail(desiredDetail.Detail.Name);

                        Cell brokeDetail = SearchBrokeDetail(brokeDetails, nameDetail);

                        if (brokeDetail != null)
                        {
                            brokeDetail.DecrementCountProduct();

                            if (brokeDetail.Count == 0)
                            {
                                brokeDetails.Remove(brokeDetail);
                            }
                        }
                        else
                        {
                            costDetail = GetDetailPrice(desiredDetail.Detail.Name);
                            money += costDetail;
                            
                            Console.WriteLine($"Заменили не ту деталь - {nameDetail}, " +
                                              $"клиенту возмещено за эту деталь - {costDetail}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Деталей не оказалось на складе, автосервис " +
                                          $"выплатит штраф в размере {_penalty}");
                        money -= _penalty;
                        
                        _brokeCars.Remove(brokeCar);
                        
                        return money;
                    }
                }
            }

            _brokeCars.Remove(brokeCar);
            
            return money;
        }

        private Cell SearchBrokeDetail(List<Cell> brokeDetails, string nameDetail)
        {
            foreach (Cell brokeDetail in brokeDetails)
            {
                if (nameDetail == brokeDetail.Detail.Name)
                {
                    return brokeDetail;
                }
            }

            return null;
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
        
        private void AddRandomMoney()
        {
            Random random = new Random();
            int minMoney = 500;
            int maxMoney = 5000;

            _money = random.Next(minMoney, maxMoney);
        }
        
        private int GetDetailPrice(string name)
        {
            int priceCheapDetail = 100;
            int priceExpensiveDetail = 200;

            DetailsBase detailPrice = new DetailsBase(priceCheapDetail, priceCheapDetail, 
                priceExpensiveDetail, priceExpensiveDetail, 
                priceExpensiveDetail, priceCheapDetail, priceExpensiveDetail,
                priceExpensiveDetail);
            
            return detailPrice.GetCount(name);
        }

        private int WorkPrice(string name, int count)
        {
            int timeForHardWork = 3;
            int timeForMediumWork = 2;
            int timeForEasyWork = 1;

            DetailsBase workPrice = new DetailsBase(
                _payPerHour * timeForMediumWork * count,
                _payPerHour * timeForEasyWork * count,
                _payPerHour * timeForEasyWork * count,
                _payPerHour * timeForMediumWork * count,
                _payPerHour * timeForEasyWork * count,
                _payPerHour * timeForHardWork * count,
                _payPerHour * timeForHardWork * count,
                _payPerHour * timeForHardWork * count);
            
            return workPrice.GetCount(name);
        }
        
        public int CalculateDetailsCost(int indexCarForService)
        {
            int cost = 0;
            List<Cell> brokeDetails = _brokeCars[indexCarForService].GetCopyBrokeDetails();
            
            foreach (Cell brokeDetail in brokeDetails)
            {
                cost += GetDetailPrice(brokeDetail.Detail.Name) * brokeDetail.Count;
            }

            return cost;
        }

        public int CalculateWorkCost(int indexCarForService)
        {
            int cost = 0;
            List<Cell> brokeDetails = _brokeCars[indexCarForService].GetCopyBrokeDetails();

            foreach (Cell brokeDetail in brokeDetails)
            {
                cost += WorkPrice(brokeDetail.Detail.Name, brokeDetail.Count);
            }
            
            return cost;
        }
    }

    class Detail
    {
        public string Name { get; private set; }
       
        public Detail(string name)
        {
            Name = name;
        }
    }

    class Cell
    {
        public Detail Detail { get; private set; }
        public int Count { get; private set; }
    
        public Cell(string name, int count)
        {
            Detail = new Detail(name);
            Count = count;
        }

        public void DecrementCountProduct()
        {
            if (Count > 0)
            {
                Count--;
            }
        }
    }
    
    class Storage
    {
        private List<Cell> _cells;

        public Storage()
        {
            _cells = new List<Cell>();
            AddCells();
        }

        public bool IsAvailableDetails(List<Cell> brokeDetails)
        {
            bool isExitst = true;
            
            foreach (Cell brokeDetail in brokeDetails)
            {
                foreach (Cell cell in _cells)
                {
                    if (cell.Detail.Name == brokeDetail.Detail.Name && cell.Count < brokeDetail.Count)
                    {
                        isExitst = false;
                    }
                }

                if (isExitst == false)
                {
                    return isExitst;
                }
            }

            return isExitst;
        }

        public string GetDetail(string desiredNameDetail)
        {
            Random random = new Random();
            int minPercent = 1;
            int maxPercent = 101;
            int maxchance = 10;
            int index;
            string nameDetail = "";
            
            int chance = random.Next(minPercent,maxPercent);

            if (chance <= maxchance)
            {
                index = random.Next(_cells.Count);
                
                foreach (Cell cell in _cells)
                {
                    if (index == 0)
                    {
                        cell.DecrementCountProduct();
                        nameDetail = cell.Detail.Name;
                        
                        if (cell.Count == 0)
                        {
                            _cells.Remove(cell);
                        }
                        
                        return nameDetail;
                    }

                    index--;
                }
            }
            
            foreach (Cell cell in _cells)
            {
                if (desiredNameDetail == cell.Detail.Name)
                {
                    cell.DecrementCountProduct();
                    nameDetail = cell.Detail.Name;
                        
                    if (cell.Count == 0)
                    {
                        _cells.Remove(cell);
                    }
                    
                    return nameDetail;
                }
            }
            
            return desiredNameDetail;
        }
        
        private void AddCells()
        {
            Random random = new Random();
            int maxCountDetails = 15;

            DetailsBase detailsNames = new DetailsBase();

            for (int i = 0; i < detailsNames.GetCountDetailsNames(); i++)
            {
                _cells.Add(new Cell(detailsNames.GetDetailName(i), random.Next(maxCountDetails)));
            }
        }
    }

    class Car
    {
        private List<Cell> BrokeDetails;
        
        public Car()
        {
            BrokeDetails = new List<Cell>();
            AddRandomBrokeDetails();
        }

        public List<Cell> GetCopyBrokeDetails()
        {
            List<Cell> brokeDetails = new List<Cell>();

            foreach (Cell brokeDetail in BrokeDetails)
            {
                brokeDetails.Add(new Cell(brokeDetail.Detail.Name, brokeDetail.Count));
            }

            return brokeDetails;
        }
        
        public void ShowBrokeDetails()
        {
            foreach (Cell brokeDetail in BrokeDetails)
            {
                Console.WriteLine($"поломка - {brokeDetail.Detail.Name}, " +
                                      $"количество деталей ремонта - {brokeDetail.Count}");
            }
        }

        private void AddRandomBrokeDetails()
        {
            Random random = new Random();
            int detailsNamesAndMinCount = 1;
            List<int> detailsMaxCount = new List<int>{4, 4, 4, 2, 2, 2, 1, 1};
            
            DetailsBase detailsNames = new DetailsBase();

            for (int i = 0; i < detailsNames.GetCountDetailsNames(); i++)
            {
                BrokeDetails.Add(new Cell(detailsNames.GetDetailName(i), 
                    random.Next(detailsNamesAndMinCount,detailsMaxCount[i])));
            }
        }
    }
}
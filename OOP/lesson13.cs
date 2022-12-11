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
            
            carService.Start();
        }
    }

    class CarService
    {
        private int _money;
        public CarMechanic Mechanic;
        private Storage _storage;
        private List<Car> _brokeCars;
        
        public CarService()
        {
            _storage = new Storage();
            Mechanic = new CarMechanic();
            _brokeCars = new List<Car>();
            AddRandomMoney();
            AddRandomBrokeCars();
        }

        public void Start()
        {
            int penalty = 50;
            int totalCost;
            int allEarnMoney = 0;
            
            while (IsQueueEmpty())
            {
                ShowBrokeCar();
                totalCost = ShowRepairCost();
                
                if (CheckAvailabilityDetails())
                {
                    totalCost -= FixCar();
                    
                    _money += totalCost;
                    allEarnMoney += totalCost;
                    
                    Console.WriteLine($"Автосервис заработал с починки машины - {totalCost}");
                }
                else
                {
                    Console.WriteLine($"Деталей не оказалось на складе, автосервис выплатит штраф в размере {penalty}");

                    if (_money - penalty > 0)
                    {
                        _money -= penalty;
                    }
                    else
                    {
                        _money -= penalty;
                        Console.WriteLine("автосервис банкрот и закрывается");
                        break;
                    }
                }
            }
            
            Console.WriteLine($"автосервис всего заработал - {allEarnMoney}");
            Console.WriteLine($"на счету автосервиса - {_money}");
        }
        
        private bool IsQueueEmpty()
        {
            return _brokeCars.Count > 0;
        }
        
        private void ShowBrokeCar()
        {
            int idFirstCarForService = 0;
            
            _brokeCars[idFirstCarForService].ShowBrokeDetails();
        }
        
        private int ShowRepairCost()
        {
            int idFirstCarForService = 0;
            int detailsCost = _brokeCars[idFirstCarForService].CalculateDetailsCost(Mechanic);
            int workCost = _brokeCars[idFirstCarForService].CalculateWorkCost(Mechanic);
            int totalCost = detailsCost + workCost;
            
            Console.WriteLine($"стоимость деталей без учета работы мастера - {detailsCost}");
            
            Console.WriteLine($"стоимость работы мастера - {workCost}");
            
            Console.WriteLine($"общая стоимость работы - {totalCost}");

            return totalCost;
        }

        private bool CheckAvailabilityDetails()
        {
            int idFirstCarForService = 0;
            Dictionary<Detail, int> brokeDetails = _brokeCars[idFirstCarForService].GetCopyBrokeDetails();

            return _storage.CheckAvailabilityDetailsInStorage(brokeDetails);
        }

        private int FixCar()
        {
            int money = 0;
            int costDetail;
            int idFirstCarForService = 0;
            Dictionary<Detail, int> brokeDetails = _brokeCars[idFirstCarForService].GetCopyBrokeDetails();
            string nameDetail;
            bool IsFind;
            
            while (brokeDetails.Count > 0)
            {
                foreach (KeyValuePair<Detail, int> desiredDetail in brokeDetails)
                {
                    nameDetail = _storage.GetDetail(desiredDetail.Key.Name);
                    IsFind = false;
                    
                    foreach (KeyValuePair<Detail, int> brokeDetail in brokeDetails)
                    {
                        if (nameDetail == brokeDetail.Key.Name)
                        {
                            IsFind = true;
                            
                            brokeDetails[brokeDetail.Key] = brokeDetail.Value - 1;

                            if (brokeDetail.Value == 0)
                            {
                                brokeDetails.Remove(brokeDetail.Key);
                            }
                            
                            break;
                        }
                    }

                    if (IsFind == false)
                    {
                        costDetail = Mechanic.DetailPrice(desiredDetail.Key.Name);
                        money += costDetail;
                            
                        Console.WriteLine($"Заменили не ту деталь - {nameDetail}, " +
                                          $"клиенту возмещено за эту деталь - {costDetail}");
                    }
                }
            }

            _brokeCars.RemoveAt(idFirstCarForService);
            
            return money;
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
    }

    class CarMechanic
    {
        private int _payPerHour;

        public CarMechanic()
        {
            _payPerHour = 20;
        }

        public int DetailPrice(string name)
        {
            int priceCheapDetail = 100;
            int priceExpensiveDetail = 200;

            int price = 0;
            
            switch (name)
            {
                case "Дверная ручка":
                    price = priceCheapDetail;
                    break;
                case "Датчик давления в шине":
                    price = priceCheapDetail;
                    break;
                case "Тормозной диск":
                    price = priceExpensiveDetail;
                    break;
                case "Передняя фара":
                    price = priceExpensiveDetail;
                    break;
                case "Задний габарит":
                    price = priceExpensiveDetail;
                    break;
                case "Аккумулятор":
                    price = priceCheapDetail;
                    break;
                case "Лобовое стекло":
                    price = priceExpensiveDetail;
                    break;
                case "Заднее стекло":
                    price = priceExpensiveDetail;
                    break;
            }
            
            return price;
        }

        public int WorkPrice(string name, int count)
        {
            int timeForHardWork = 3;
            int timeForMediumWork = 2;
            int timeForEasyWork = 1;
            
            int price = 0;
            
            switch (name)
            {
                case "Дверная ручка":
                    price = _payPerHour * timeForMediumWork * count;
                    break;
                case "Датчик давления в шине":
                    price = _payPerHour * timeForEasyWork * count;
                    break;
                case "Тормозной диск":
                    price = _payPerHour * timeForEasyWork * count;
                    break;
                case "Передняя фара":
                    price = _payPerHour * timeForEasyWork * count;
                    break;
                case "Задний габарит":
                    price = _payPerHour * timeForEasyWork * count;
                    break;
                case "Аккумулятор":
                    price = _payPerHour * timeForEasyWork * count;
                    break;
                case "Лобовое стекло":
                    price = _payPerHour * timeForHardWork * count;
                    break;
                case "Заднее стекло":
                    price = _payPerHour * timeForHardWork * count;
                    break;
            }
            
            return price;
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

    class Storage
    {
        private Dictionary<Detail, int> _details;

        public Storage()
        {
            _details = new Dictionary<Detail, int>();
            AddRandomStorage();
        }

        public bool CheckAvailabilityDetailsInStorage(Dictionary<Detail, int> brokeDetails)
        {
            bool isExitst = true;
            
            foreach (KeyValuePair<Detail, int> brokeDetail in brokeDetails)
            {
                foreach (KeyValuePair<Detail, int> detail in _details)
                {
                    if (detail.Key.Name == brokeDetail.Key.Name && detail.Value < brokeDetail.Value)
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
                index = random.Next(_details.Count);
                
                foreach (KeyValuePair<Detail, int> detail in _details)
                {
                    if (index == 0)
                    {
                        _details[detail.Key] = detail.Value - 1;
                        nameDetail = detail.Key.Name;
                        
                        if (detail.Value == 0)
                        {
                            _details.Remove(detail.Key);
                        }
                        
                        return nameDetail;
                    }

                    index--;
                }
            }
            
            foreach (KeyValuePair<Detail, int> detail in _details)
            {
                if (desiredNameDetail == detail.Key.Name)
                {
                    _details[detail.Key] = detail.Value - 1;
                    nameDetail = detail.Key.Name;
                        
                    if (detail.Value == 0)
                    {
                        _details.Remove(detail.Key);
                    }
                    
                    break;
                }
            }
            
            return nameDetail;
        }
        
        private void AddRandomStorage()
        {
            Random random = new Random();
            int maxCountDetails = 15;
            
            List<string> detailsNames = new List<string>()
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

            for (int i = 0; i < detailsNames.Count; i++)
            {
                _details.Add(new Detail(detailsNames[i]), random.Next(maxCountDetails));
            }
        }
    }

    class Car
    {
        private Dictionary<Detail, int> _brokeDetails;
        
        public Car()
        {
            _brokeDetails = new Dictionary<Detail, int>();
            AddRandomBrokeDetails();
        }

        public Dictionary<Detail, int> GetCopyBrokeDetails()
        {
            Dictionary<Detail, int> brokeDetails = new Dictionary<Detail, int>();

            foreach (KeyValuePair<Detail, int> brokeDetail in _brokeDetails)
            {
                brokeDetails.Add(new Detail(brokeDetail.Key.Name), brokeDetail.Value);
            }

            return brokeDetails;
        }
        
        public void ShowBrokeDetails()
        {
            foreach (KeyValuePair<Detail, int> brokeDetail in _brokeDetails)
            {
                Console.WriteLine($"поломка - {brokeDetail.Key.Name}, " +
                                      $"количество деталей ремонта - {brokeDetail.Value}");
            }
        }

        public int CalculateDetailsCost(CarMechanic mechanic)
        {
            int cost = 0;

            foreach (var brokeDetail in _brokeDetails)
            {
                cost += mechanic.DetailPrice(brokeDetail.Key.Name) * brokeDetail.Value;
            }

            return cost;
        }

        public int CalculateWorkCost(CarMechanic mechanic)
        {
            int cost = 0;

            foreach (var brokeDetail in _brokeDetails)
            {
                cost += mechanic.WorkPrice(brokeDetail.Key.Name, brokeDetail.Value);
            }
            
            return cost;
        }

        private void AddRandomBrokeDetails()
        {
            Random random = new Random();
            
            Dictionary<string, int> detailsNamesAndMaxCount = new Dictionary<string, int>()
            {
                {"Дверная ручка", 4},
                {"Датчик давления в шине", 4},
                {"Тормозной диск", 4},
                {"Передняя фара", 2},
                {"Задний габарит", 2},
                {"Аккумулятор", 2},
                {"Лобовое стекло", 1},
                {"Заднее стекло", 1}
            };

            foreach (KeyValuePair<string, int> detailsNameAndMaxCount in detailsNamesAndMaxCount)
            {
                int countBrokeDetails = random.Next(detailsNameAndMaxCount.Value);

                if (countBrokeDetails > 0)
                {
                    _brokeDetails.Add(new Detail(detailsNameAndMaxCount.Key), countBrokeDetails);
                }
            }
        }
    }
}
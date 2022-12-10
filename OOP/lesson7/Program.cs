using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Scoreboard scoreboard = new Scoreboard();
            TickerWindow tickerWindow = new TickerWindow();
            
            tickerWindow.ChooseCommand(scoreboard);
        }
    }

    class TickerWindow
    {
        private const string CommandCreateTrip = "1";
        private const string CommandExit = "0";

        public void ChooseCommand(Scoreboard scoreboard)
        {
            bool isOpen = true;
            
            while (isOpen)
            {
                scoreboard.Show();
                
                Console.Write($"\n{CommandCreateTrip} - создать направление" +
                              $"\n{CommandExit} - выход из программы" +
                              "\nВыберите команду ");
                
                string choose = Console.ReadLine();

                switch (choose)
                {
                    case CommandCreateTrip:
                        scoreboard.CreateTrip();
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
    }
    
    class Scoreboard
    {
        private List<Train> _trains;

        public Scoreboard()
        {
            _trains = new List<Train>();
        }

        public void Show()
        {
            foreach (Train train in _trains)
            {
                Console.WriteLine($"откуда - {train.DepartureFrom} куда - {train.DepartureTo}\n" +
                                  $"количество пассажиров - {train.CountPassengers} " +
                                  $"количество вагонов - {train.GetCountCarriages()}");
            }

            if (_trains.Count == 0)
            {
                Console.WriteLine("рейсы отсутствуют");
            }
        }
        
        public void CreateTrip()
        {
            int minPassengers = 100;
            int maxPassengers = 500;
            
            Console.Write("введите откуда едет поезд: ");
            string departureFrom = Console.ReadLine();
            
            Console.Write("введите куда едет поезд: ");
            string departureTo = Console.ReadLine();

            Random random = new Random();
            int countPassengers = random.Next(minPassengers, maxPassengers);
            
            _trains.Add(new Train(departureFrom, departureTo, countPassengers));
            
            _trains[_trains.Count - 1].SeatPassengers();
        }
    }
    
    class Carriage
    {
        private List<int> _listCountSeats;
        public int RandomCountSeats { get; private set; }
        
        public Carriage()
        {
            _listCountSeats = new List<int>() {36, 54};
            Random random = new Random();
            RandomCountSeats = _listCountSeats[random.Next(0, _listCountSeats.Count)];
        }
    }

    class Train
    {
        private List<Carriage> _сarriages;
        public string DepartureFrom { get; private set; }
        public string DepartureTo { get; private set; }
        public int CountPassengers { get; private set; }

        public Train(string departureFrom, string departureTo, int countPassengers)
        {
            _сarriages = new List<Carriage>();
            DepartureFrom = departureFrom;
            DepartureTo = departureTo;
            CountPassengers = countPassengers;
        }
        
        public int GetCountCarriages()
        {
            return _сarriages.Count;
        }
        
        public void SeatPassengers()
        {
            int unseatedPassengers = CountPassengers;

            while (unseatedPassengers > 0)
            {
                Carriage carriage = new Carriage();
                unseatedPassengers -= carriage.RandomCountSeats;
                _сarriages.Add(carriage);
            }
        }
    }
}
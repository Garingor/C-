using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            int countClients = 20;
            List<string> productNames = new List<string>() {"картофель", "рыба", "молоко", "яйца", "орехи", 
                "сахар", "макароны", "капуста", "печенье", "газировка"};
            
            Supermarket supermarket = new Supermarket(countClients, productNames);
            
            supermarket.Work();
        }
    }
    
    class Client
    {
        private List<Product> _products;
        public int Money { get; private set; }
        
        public Client(int money, List<string> productNames)
        {
            Money = money;
            AddRandomProducts(productNames);
        }
        
        public void Pay(int purchaseAmountOfProducts)
        {
            Money -= purchaseAmountOfProducts;
            
            ShowLog(purchaseAmountOfProducts);
        }
        
        public int CountAllProductPrice()
        {
            int finalprice = 0;
            
            foreach (Product product in _products)
            {
                finalprice += product.Price;
            }

            return finalprice;
        }

        public int DecreaseFinalPrice(int finalPriceOfProducts)
        {
            Random random = new Random();
            
            while (Money < finalPriceOfProducts && _products.Count > 0)
            {
                int idProduct = random.Next(0, _products.Count);
                finalPriceOfProducts -= _products[idProduct].Price;
                _products.RemoveAt(idProduct);
            }

            return finalPriceOfProducts;
        }

        private void AddRandomProducts(List<string> productNames)
        {
            _products = new List<Product>();
            int minPrice = 100;
            int maxPrice = 500;
            Random random = new Random();

            for (int i = 0; i < productNames.Count; i++)
            {
                _products.Add(new Product(productNames[i], 
                    random.Next(minPrice, maxPrice)));
            }
        }

        private void ShowLog(int purchaseAmountOfProducts)
        {
            Console.WriteLine();
            
            foreach (Product product in _products)
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }
            
            Console.WriteLine($"Покупатель заплатил - {purchaseAmountOfProducts}, денег осталось - {Money}");
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
    
    class Supermarket
    {
        private List<Client> _clients;
        private List<string> _productNames;
        
        public Supermarket(int queueLength, List<string> productNames)
        {
            _clients = new List<Client>();
            _productNames = productNames;
            AddRandomQueue(queueLength);
        }

        public void Work()
        {
            int finalPriceOfProducts;

            foreach (Client client in _clients)
            {
                finalPriceOfProducts = client.CountAllProductPrice();

                finalPriceOfProducts = client.DecreaseFinalPrice(finalPriceOfProducts);

                if (finalPriceOfProducts != 0)
                {
                    client.Pay(finalPriceOfProducts);
                }
                else
                {
                    Console.WriteLine("\nнедостаточно денег у клиента");
                }
            }
        }
        
        private void AddRandomQueue(int queueLength)
        {
            int minMoney = 500;
            int maxMoney = 1000;
            Random random = new Random();
            
            for (int i = 0; i < queueLength; i++)
            {
                _clients.Add(new Client(random.Next(minMoney, maxMoney), _productNames));
            }
        }
    }
}
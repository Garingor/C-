using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int minPurchaseAmount = 50;
            int maxPurchaseAmount = 1500;
            int numberOfPurchases = 10;

            int account = 0;
            
            Queue<int> queue = new Queue<int>();
            
            AddRandomPurchaseAmounts(queue, minPurchaseAmount, maxPurchaseAmount, numberOfPurchases);

            account = ServiceCustomers(queue, account);
        }

        static int ServiceCustomers(Queue<int> queue, int account)
        {
            int queueLength = queue.Count;
            
            for (int i = 0; i < queueLength; i++)
            {
                account += queue.Dequeue();
                
                Console.WriteLine($"сейчас на счету - {account}");

                Console.ReadKey();
                Console.Clear();
            }

            return account;
        }

        static void AddRandomPurchaseAmounts(Queue<int> queue, int minPurchaseAmount, int maxPurchaseAmount, 
            int numberOfPurchases)
        {
            Random random = new Random();
            
            for (int i = 0; i < numberOfPurchases; i++)
            {
                queue.Enqueue(random.Next(minPurchaseAmount, maxPurchaseAmount));
            }
        }
    }
}
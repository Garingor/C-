using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            Player player = new Player();
            Deck deck = new Deck();;
            
            menu.ChooseCountCardsInDeck(deck);
            
            menu.ChooseCommands(deck, player);
        }
    }

    class Menu
    {
        private const string CommandTakeByCount = "1";
        private const string CommandTakeByClick = "2";
        private const string CommandExit = "0";
        
        private bool _isExit;

        public Menu()
        {
            _isExit = false;
        }

        public void ChooseCountCardsInDeck(Deck deck)
        {
            while (_isExit == false)
            {
                Console.Write($"Введите количество карт в колоде ({Deck.CountSmall} или {Deck.CountBig}): ");

                if (int.TryParse(Console.ReadLine(), out int countCardsInDeck) && 
                    (countCardsInDeck == Deck.CountSmall || countCardsInDeck == Deck.CountBig))
                {
                    deck.Init(countCardsInDeck);
                    _isExit = true;
                }
                else
                {
                    Console.WriteLine("Неверный ввод, попробуйте еще раз");
                }
            }

            _isExit = false;
        }

        public void ChooseCommands(Deck deck, Player player)
        {
            while (_isExit == false)
            {
                Console.Write($"\n{CommandTakeByCount} - достать определенное количество карт" +
                              $"\n{CommandTakeByClick} - достать карты по кнопке" +
                              $"\n{CommandExit} - выход из программы" +
                              "\nВыберите команду ");

                string choose = Console.ReadLine();

                switch (choose)
                {
                    case CommandTakeByCount:
                        deck.TakeByCount(player);
                        break;
                    case CommandTakeByClick:
                        deck.TakeByClick(player);
                        break;
                    case CommandExit:
                        _isExit = true;
                        break;
                    default:
                        Console.WriteLine("команда не распознана, попробуйте еще раз");
                        break;
                }
            }
            
            _isExit = false;
        }
    }
    
    class Card
    {
        private string _rating;
        private string _suit;
        
        public Card(string rating, string suit)
        {
            _rating = rating;
            _suit = suit;
        }

        public void Show()
        {
            Console.WriteLine($"масть - {_suit} карта - {_rating}");
        }
    }

    class Player
    {
        private List<Card> _cards;

        public Player()
        {
            _cards = new List<Card>();
        }
        
        public void ShowLastCardInHand()
        {
            _cards[_cards.Count - 1].Show();
        }
        
        public void ShowCards()
        {
            foreach (var card in _cards)
            {
                card.Show();
            }
        }

        public void TakeCard(Card card)
        {
            _cards.Add(card);
        }
    }

    class Deck
    {
        public const int CountSmall = 36;
        public const int CountBig = 52;
        
        private int _minCardRating;
        private int _maxCardRating;
        private int _indexCurrentCard;
        private List<Card> _cards;
        private List<string> _names;
        private List<string> _suits;

        public Deck()
        {
            _indexCurrentCard = 0;
            _maxCardRating = 11;
            _names = new List<string>() { "2","3","4","5","6","7","8","9","10", "Валет", "Дама", "Король", "Туз" };
            _suits = new List<string>() { "черви", "пики", "бубны", "трефы"};
            _cards = new List<Card>();
        }

        public void Init(int countCards)
        {
            if (countCards == CountSmall)
            {
                _minCardRating = 4;

                for (int i = _minCardRating; i < _maxCardRating; i++)
                {
                    _cards.Add(new Card(_names[i],_suits[0]));
                    _cards.Add(new Card(_names[i],_suits[1]));
                    _cards.Add(new Card(_names[i],_suits[2]));
                    _cards.Add(new Card(_names[i],_suits[3]));
                }
            }
            else if (countCards == CountBig)
            {
                _minCardRating = 0;

                for (int i = _minCardRating; i < _maxCardRating; i++)
                {
                    _cards.Add(new Card(_names[i],_suits[0]));
                    _cards.Add(new Card(_names[i],_suits[1]));
                    _cards.Add(new Card(_names[i],_suits[2]));
                    _cards.Add(new Card(_names[i],_suits[3]));
                }
            }
            
            ShuffleCards();
        }

        public Card GiveCard()
        {
            Card card = _cards[_indexCurrentCard];
            _indexCurrentCard++;

            if (_indexCurrentCard == _cards.Count - 1)
            {
                ShuffleCards();
                _indexCurrentCard = 0;
            }
            
            return card;
        }

        public void TakeByCount(Player player)
        {
            Card card;
            Console.Write("Сколько карт достать ");

            if (int.TryParse(Console.ReadLine(), out int countCards))
            {
                for (int i = 0; i < countCards; i++)
                {
                    card = GiveCard();
                    player.TakeCard(card);
                }
                
                player.ShowCards();
            }
            else
            {
                Console.WriteLine("команда не распознана, попробуйте еще раз");
            }
        }

        public void TakeByClick(Player player)
        {
            Card card;
            ConsoleKey enter = ConsoleKey.Enter;
            
            Console.WriteLine("Чтобы вытаскивать карты нажмите любую кнопку, для остановки нажмите Enter");
                    
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            
            while (keyInfo.Key != enter)
            {
                card = GiveCard();
                player.TakeCard(card);
                player.ShowLastCardInHand();
                keyInfo = Console.ReadKey(true);
            }
        }
        
        private void ShuffleCards()
        {
            Random random = new Random();
            int index;
            
            for (int i = 0; i < _cards.Count; i++)
            {
                index = random.Next(0,_cards.Count);
                (_cards[i], _cards[index]) = (_cards[index], _cards[i]);
            }
        }
    }
}
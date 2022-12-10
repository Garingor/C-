using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddBook = "1";
            const string CommandDeleteBook = "2";
            const string CommandShowAllBooks = "3";
            const string CommandShowBookByParameter = "4";
            const string CommandExit = "0";

            bool isExit = false;
            string choose;

            Storage storage = new Storage();
            
            while (isExit == false)
            {
                Console.Write($"\n{CommandAddBook} - добавить книгу" +
                              $"\n{CommandDeleteBook} - удалить книгу" + 
                              $"\n{CommandShowAllBooks} - показать все книги" + 
                              $"\n{CommandShowBookByParameter} - показать все книги по параметру" +
                              $"\n{CommandExit} - выход из программы" +
                              "\nвыберите команду: ");

                choose = Console.ReadLine();

                switch (choose)
                {
                    case CommandAddBook:
                        storage.AddBook();
                        break;
                    case CommandDeleteBook:
                        storage.DeleteBook();
                        break;
                    case CommandShowAllBooks:
                        storage.ShowBooks();
                        break;
                    case CommandShowBookByParameter:
                        storage.ShowBooksByParameter();
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
    }

    class Book
    {
        public string Name { get; private set; }
        public string Author { get; private set; }
        public int ReleaseYear { get; private set; }

        public Book(string name, string author, int releaseYear)
        {
            Name = name;
            Author = author;
            ReleaseYear = releaseYear;
        }
    }

    class Storage
    {
        private const string CommandShowByParameterName = "1";
        private const string CommandShowByParameterAuthor = "2";
        private const string CommandShowByParameterReleaseYear = "3";
        
        private List<Book> _books;

        public Storage()
        {
            _books = new List<Book>();
        }

        public void AddBook()
        {
            Console.Write("\nвведите название книги: ");
            
            string name = Console.ReadLine();
            
            Console.Write("\nвведите автора книги: ");
            
            string author = Console.ReadLine();
            
            Console.Write("\nвведите год выпуска книги: ");
            
            if (int.TryParse(Console.ReadLine(), out int releaseYear))
            {
                _books.Add(new Book(name, author, releaseYear));
            }
            else
            {
                Console.WriteLine("\nне удалось добавить книгу");
            }
        }
        
        public void DeleteBook()
        {
            bool isDelete = false;
            
            Console.Write("\nвведите название книги: ");
            
            string name = Console.ReadLine();

            foreach (Book book in _books)
            {
                if (book.Name.ToLower() == name.ToLower())
                {
                    _books.Remove(book);
                    isDelete = true;
                    break;
                }
            }

            if (isDelete == false)
            {
                Console.WriteLine("\nне удалось найти и удалить книгу");
            }
        }

        public void ShowBook(Book book)
        {
            Console.WriteLine($"\nназвание - {book.Name}\nавтор - {book.Author}\nгод выпуска - {book.ReleaseYear}\n");
        }
        
        public void ShowBooks()
        {
            foreach (Book book in _books)
            {
                ShowBook(book);
            }
        }
        
        public void ShowBooksByParameter()
        {
            Console.Write($"\n{CommandShowByParameterName} - по имени" +
                          $"\n{CommandShowByParameterAuthor} - по автору" +
                          $"\n{CommandShowByParameterReleaseYear} - по дате выпуска" +
                          "\nвведите параметр поиска книг: ");
            
            string parametr = Console.ReadLine();

            switch (parametr)
            {
                case CommandShowByParameterName:
                    SearchByName();
                    break;
                case CommandShowByParameterAuthor:
                    SearchByAuthor();
                    break;
                case CommandShowByParameterReleaseYear:
                    SearchByReleaseYear();
                    break;
                default:
                    Console.WriteLine("\nкоманда не распознана, попробуйте еще раз");
                    break;
            }
        }

        private void SearchByName()
        {
            Console.Write("\nвведите название книг: ");
            
            string name = Console.ReadLine();
            
            foreach (Book book in _books)
            {
                if (book.Name.ToLower() == name)
                {
                    ShowBook(book);
                }
            }
        }
        
        private void SearchByAuthor()
        {
            Console.Write("\nвведите автора книги: ");
            
            string author = Console.ReadLine();
            
            foreach (Book book in _books)
            {
                if (book.Author.ToLower() == author)
                {
                    ShowBook(book);
                }
            }
        }
        
        private void SearchByReleaseYear()
        {
            Console.Write("\nвведите год выпуска книг: ");
            
            if (int.TryParse(Console.ReadLine(), out int releaseYear))
            {
                foreach (Book book in _books)
                {
                    if (book.ReleaseYear == releaseYear)
                    {
                        ShowBook(book);
                    }
                }
            }
            else
            {
                Console.WriteLine("\nне удалось считать год выпуска книг");
            }
        }
    }
}
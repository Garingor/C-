using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Renderer renderer = new Renderer();
            
            renderer.DrawPlayer(player.PositionX, player.PositionY);
        }

        class Player
        {
            public int PositionX { get; private set; }
            public int PositionY { get; private set; }
            
            public Player()
            {
                PositionX = 5;
                PositionY = 5;
            }
            
            public Player(int positionX, int positionY)
            {
                PositionX = positionX;
                PositionY = positionY;
            }
        }

        class Renderer
        {
            public void DrawPlayer(int positionX, int positionY, char character = '@')
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.Write(character);
            }
        }
    }
}
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] map = ReadMap("map.txt.txt");
            bool isActive = true;
            ConsoleKeyInfo key = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            Task.Run(() =>
            {
                while (isActive)
                {
                    key = Console.ReadKey();
                }
            });

            int packmanX = 1;
            int packmanY = 1;
            int score = 0;

            while (isActive)
            {
                Console.Clear();
                HandeleInput(key, ref packmanX, ref packmanY, map, ref score);


                Console.ForegroundColor = ConsoleColor.Green;
                DrawMap(map);

                Console.SetCursorPosition(packmanX, packmanY);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("@");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(46, 0);
                Console.Write($"Счет: {score}");

                Thread.Sleep(1000);

            }
        }

        private static char[,] ReadMap(string path, int count = 0) 
        {
            string[] file = File.ReadAllLines(path);

            char[,] map = new char[GetMaxLenghtOfLine(file), file.Length];


            for (int x = 0; x < map.GetLength(0); x++) 
                for (int y = 0; y < map.GetLength(1); y++) 
                    map[x, y] = file[y][x];
            return map;
        }

        private static void DrawMap(char[,] map) 
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(map[x, y]);
                }
            Console.Write("\n");
            }

        }


        private static void HandeleInput(ConsoleKeyInfo key, ref int packmanX, ref int packmanY, char [,] map, ref int score) 
        {
            int [] direction = GetDirection(key);
            int nextPositionX = packmanX + direction[0];
            int nextPositionY = packmanY + direction[1];

            char nextCell = map[nextPositionX, nextPositionY];

            if (nextCell == ' ' || nextCell == '.')
            {
                packmanX = nextPositionX;
                packmanY = nextPositionY;

                if (nextCell == '.') 
                {
                    score += 100;
                    map[nextPositionX, nextPositionY] = ' ';
                }
            }
        }

        private static int[] GetDirection(ConsoleKeyInfo key) 
        {
            int[] direction = { 0, 0 };

            if (key.Key == ConsoleKey.UpArrow)
                direction[1] = -1;
            else if (key.Key == ConsoleKey.DownArrow)
                direction[1] = 1;
            else if (key.Key == ConsoleKey.LeftArrow)
                direction[0] = -1;
            else if (key.Key == ConsoleKey.RightArrow)
                direction[0] = 1;
            return direction;
        }

        private static int GetMaxLenghtOfLine(string[] lines)
        {
            int maxLenght = lines[0].Length;
            foreach (var line in lines) 
                if (line.Length > maxLenght)
                    maxLenght = line.Length;
            return maxLenght;
        }
    }
}

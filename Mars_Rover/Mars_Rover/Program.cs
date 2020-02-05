using System;
using System.Collections.Generic;

namespace Mars_Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> plateauUpperRightCoordinates = new List<int>();
            List<string> inputRows = new List<string>();

            Console.WriteLine("Gezegenin boyutunu aralarında boşluk olacak şekilde giriniz (X,Y)");
            foreach (var x in Console.ReadLine().Split(' '))
            {
                plateauUpperRightCoordinates.Add(int.Parse(x));
            }

            Console.WriteLine("Araçların bilgilerini giriniz (Hesaplamaya başlamak için 'C' giriniz ! )");
            string tempInput;
            do
            {
                tempInput = Console.ReadLine();
                if (tempInput.Length > 0 && tempInput.ToUpper() != "C")
                    inputRows.Add(tempInput);
            }
            while (tempInput.ToUpper() != "C");


            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < inputRows.Count; i += 2)
            {
                var firstRowOfInput = inputRows[i].Split(' ');
                var tempRover = new Rover
                {
                    X = int.Parse(firstRowOfInput[0]),
                    Y = int.Parse(firstRowOfInput[1]),
                    Direction = firstRowOfInput[2]
                };
                var secondRowOfInput = inputRows[i + 1].ToUpper().ToCharArray();
                int test3 = 0;

                foreach (var command in secondRowOfInput)
                {
                    if (command == 'L' || command == 'R')
                    {
                        var nextDirection = findNextDirection(command, tempRover);
                        if (nextDirection != "I")
                            tempRover.Direction = nextDirection;
                    }
                    if (command == 'M')
                    {
                        tempRover = move(tempRover, plateauUpperRightCoordinates);
                    }
                    test3++;

                }
                var test = tempRover.X.ToString() + ' ' + tempRover.Y.ToString() + ' ' + tempRover.Direction;

                Console.WriteLine(test);
            }


            Console.ReadKey();
        }

        private static Rover move(Rover tempRover, List<int> plateauUpperRightCoordinates)
        {
            switch (tempRover.Direction)
            {
                case "N":
                    if (tempRover.Y < plateauUpperRightCoordinates[1])  // Gezegenin sınırına geldiğinde dışarıya hareket edememeli
                        tempRover.Y += 1;
                    return tempRover;
                case "E":
                    if (tempRover.X < plateauUpperRightCoordinates[0])
                        tempRover.X += 1;
                    return tempRover;
                case "S":
                    if (tempRover.Y > 0)
                        tempRover.Y -= 1;
                    return tempRover;
                case "W":
                    if (tempRover.X > 0)
                        tempRover.X -= 1;
                    return tempRover;
                default:
                    return tempRover;
            }
        }

        private static string findNextDirection(char command, Rover tempRover)
        {
            if (command == 'L')
            {
                switch (tempRover.Direction)
                {
                    case "N":
                        return "W";
                    case "E":
                        return "N";
                    case "S":
                        return "E";
                    case "W":
                        return "S";
                    default:
                        return "I"; // Invalid Direction 
                }
            }
            else
            {
                switch (tempRover.Direction)
                {
                    case "N":
                        return "E";
                    case "E":
                        return "S";
                    case "S":
                        return "W";
                    case "W":
                        return "N";
                    default:
                        return "I"; // Invalid Direction 
                }
            }

        }
    }
    public class Rover
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; }
    }
}
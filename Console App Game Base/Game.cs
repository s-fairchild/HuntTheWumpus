using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus
{
    class Game
    {
        public static Dictionary<int, string> MapSize = new Dictionary<int, string>()
        {
            {1, "small"},
            {2, "medium"},
            {3, "large"}
        };
        public static string size = "";

        static int GameState = 0;

        public static void Menu()
        {
            Console.WriteLine("Welcome to Hunt the Wumpus!");
            Console.WriteLine("Please select the size of the map: " +
                "\n1: small" +
                "\n2: medium" +
                "\n3: large");
            string input = Console.ReadLine();
            Game.size = MapSize[int.Parse(input)];
        }
        public static void Play()
        {
            do
            {
                Update();
                Action();
            } while (GameState == 0);
        }

        public static void Update()
        {
            //Console.Clear();
            Map.printCaverns(size);
            Console.WriteLine($"you're in Cavern: {Map.PlayerCurrentLoc()}");
        }
        public static void Action()
        {
            Console.WriteLine("Please choose your action: ");
            Console.WriteLine("1: Move " +
                "\n2: Shoot");
            string ActionInput = Console.ReadLine();
            switch (ActionInput)
            {
                case "1":
                    MoveShootMenu();
                    GameState = Map.Move(size);
                    break;
                case "2":
                    MoveShootMenu();
                    GameState = Map.Shoot(size);
                    break;
                default:
                    Console.Clear();
                    Action();
                    break;
            }
        }
        public static void MoveShootMenu()
        {
            Console.WriteLine("n: Move up" +
                "\ns: Move down" +
                "\ne: Move right" +
                "\nw: Move left");
        }

    }
}

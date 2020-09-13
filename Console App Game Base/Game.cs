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
            
            Console.WriteLine("===========================\nWelcome to Hunt the Wumpus!\n===========================\n");
            Console.WriteLine("Please select the size of the map: " +
                "\n----------------------------------" +
                "\n1: small" +
                "\n2: medium" +
                "\n3: large");
            string input = Console.ReadLine();
            size = MapSize[int.Parse(input)];
        }
        public static void Play()
        {
            do
            {
                Update();
                Action();
            } while (GameState == 0);
        }
        public static string PlayerPreviousLocation = "";
        public static void Update()
        {
            Console.Clear();
            Map.printCaverns(size);
            Console.WriteLine($"==========================\nYou were in cavern: {PlayerPreviousLocation}");
            Console.WriteLine($"You are in Cavern:  {Map.PlayerCurrentLoc()}\n==========================");
                PlayerPreviousLocation = Map.PlayerCurrentLoc();
            Map.checkCavern(size);
        }
        public static void Action()
        {
            Console.WriteLine("Please choose your action: \n--------------------------");
            Console.WriteLine("1: Move " +
                "\n2: Shoot" +
                "\n3: Note" +
                "\n4: Trace" +
                "\n5: Cheat" +
                "\n--------------------------");
            string ActionInput = Console.ReadLine();
            Console.WriteLine();
            switch (ActionInput)
            {
                case "1":
                    MoveMenu();
                    GameState = Map.Move(size);
                    break;
                case "2":
                    ShootMenu();
                    GameState = Map.Shoot(size);
                    break;
                case "3":
                    Map.Note(size);
                    Console.WriteLine("enter any charater to get back in action");
                    Console.ReadLine();
                    Update();
                    break;
                case "4":
                    Map.Trace(size);
                    Console.WriteLine("enter any charater to get back in action");
                    Console.ReadLine();
                    Update();
                    break;
                case "5":
                    Map.Cheat(size);
                    Console.WriteLine("enter any charater to get back in action");
                    Console.ReadLine();
                    Update();
                    break;
                default:
                    Console.Clear();
                    Update();
                    break;
            }
        }
        public static void MoveMenu()
        {
            Console.WriteLine("Please Choose your direction: \n-------------------------" +
                "\n1: Move up" +
                "\n2: Move down" +
                "\n3: Move right" +
                "\n4: Move left" +
                "\n-------------------------");
        }
        public static void ShootMenu()
        {
            Console.WriteLine("Please CHoose your direction: \n-------------------------" +
                "\n1: Shoot above" +
                "\n2: Shoot below" +
                "\n3: shoot right" +
                "\n4: shoot left" +
                "\n-------------------------");
        }
    }
}

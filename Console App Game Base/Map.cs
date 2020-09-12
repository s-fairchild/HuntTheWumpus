using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus
{
    public class Map
    {
        public static Dictionary<string, int> MapSize = new Dictionary<string, int>()  
        {
            {"small", 6},
            {"medium", 7},
            {"large", 8}
        };

        public static string PlayerLocation; 
        
        static int numberOfPits = 0;
        static int numberOfBats = 0;
       
        static int PlayerCurrentCol = 0;
        static int PlayerCurrentRow = 0;
        
        static Random Loc = new Random();

        public Map(string size)
        {
            switch (MapSize[size])
            {
                case 6:
                    numberOfPits = 2;
                    numberOfBats = 2;
                    break;
                case 7:
                    numberOfPits = 3;
                    numberOfBats = 3;
                    break;
                case 8:
                    numberOfPits = 4;
                    numberOfBats = 4;
                    break;
            }
        }

        static Cavern[,] newMap; 

        public static void InitializeMap(string size)
        {
            newMap = new Cavern[MapSize[size], MapSize[size]];
            for (int i = 0; i < MapSize[size]; i++)
            {
                for (int j = 0; j < MapSize[size]; j++)
                {
                    newMap[j,i] = new Cavern(j, i);
                }
            }            
        }
        public static void assignWumpus(string size)
        {
            int col = Loc.Next(MapSize[size]);
            int row = Loc.Next(MapSize[size]);

            newMap[col, row].Wampus = true;
            newMap[col, row].Occupied = true;
            populateBlood(col, row, size);
        }
        public static void populatePlayer(string size)
        {
            int col = Loc.Next(MapSize[size]);
            int row = Loc.Next(MapSize[size]);

            if (newMap[col, row].Occupied == false)
            {
                newMap[col, row].Player = true;
                newMap[col, row].Occupied = true;
                PlayerCurrentCol = col;
                PlayerCurrentRow = row;
            }
            else
                populatePlayer(size);
        }
        public static void populatePits(string size)
        {
            int col = Loc.Next(MapSize[size]);
            int row = Loc.Next(MapSize[size]);

            if (newMap[col, row].Occupied == false)
            {
                newMap[col, row].Pit = true;
                newMap[col, row].Occupied = true;
                populateDraft(col, row, size);
            }
            else
                populatePits(size);
        }
        public static void populateBats(string size)
        {
            int col = Loc.Next(MapSize[size]);
            int row = Loc.Next(MapSize[size]);

            if (newMap[col, row].Occupied == false)
            {
                newMap[col, row].Bats = true;
                newMap[col, row].Occupied = true;
            }
            else
                populateBats(size);
        }
        public static void populateBlood(int col, int row, string size )
        {
            int newCol = col + 1;
            if (newCol > MapSize[size] - 1)
                newCol = 0;
            newMap[newCol, row].Blood = true;
            newCol = col - 1;
            if (newCol < 0)
                newCol = MapSize[size] - 1;
            newMap[newCol, row].Blood = true;
            int newRow = row + 1;
            if (newRow > MapSize[size] - 1)
                newRow = 0;
            newMap[col, newRow].Blood = true;
            newRow = row - 1;
            if (newRow < 0)
                newRow = MapSize[size] - 1;
            newMap[col, newRow].Blood = true;
        }
        public static void populateDraft(int col, int row, string size)
        {

            int newCol = col + 1;
            if (newCol > MapSize[size] - 1)
                newCol = 0;
            newMap[newCol, row].Draft = true;
            newCol = col - 1;
            if (newCol < 0)
                newCol = MapSize[size] - 1;
            newMap[newCol, row].Draft = true;
            int newRow = row + 1;
            if (newRow > MapSize[size] - 1)
                newRow = 0;
            newMap[col, newRow].Draft = true;
            newRow = row - 1;
            if (newRow < 0)
                newRow = MapSize[size] - 1;
            newMap[col, newRow].Draft = true;
        }
        public static void printINFO(string size)
        {
            for (int i = 0; i < MapSize[size]; i++)
            {
                for (int j = 0; j < MapSize[size]; j++)
                {
                    if (newMap[j, i].Occupied == true | newMap[j, i].Blood == true | newMap[j,i].Draft == true | newMap[j, i].Bats == true)
                    {
                        Console.WriteLine($"[{newMap[j, i].Column},{newMap[j, i].Row}]\nwumpus: {newMap[j, i].Wampus}\nplayer: {newMap[j, i].Player}\npit: {newMap[j, i].Pit}\nblood: {newMap[j, i].Blood}\ndraft: {newMap[j, i].Draft}\nbats: {newMap[j, i].Bats}");
                        Console.WriteLine();
                    }
                }
            }
        }
        public static void printCaverns(string size)
        {
            for (int i = 0; i < MapSize[size]; i++)
            {
                for (int j = 0; j < MapSize[size]; j++)
                {
                        Console.Write($"[{newMap[j, i].Column},{newMap[j, i].Row}]");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void Initialize(string size)
        {
            Map map = new Map(size);
            InitializeMap(size);
            printCaverns(size);
            assignWumpus(size);
            populatePlayer(size);
            for (int i = 0; i < numberOfPits; i++)
                populatePits(size);
            for (int i = 0; i < numberOfBats; i++)
                populateBats(size);
            printINFO(size);
        }
        public static int Shoot(string size)
        {
            string direction = Console.ReadLine();
            switch (direction)
            {
                case "1":
                    int updatedRow = PlayerCurrentRow - 1;
                    if (updatedRow < 0)
                        updatedRow = MapSize[size] - 1;
                    return CheckIfWumpasDead(PlayerCurrentCol, updatedRow);
                case "2":
                    updatedRow = PlayerCurrentRow + 1;
                    if (updatedRow > MapSize[size] - 1)
                        updatedRow = 0;
                    return CheckIfWumpasDead(PlayerCurrentCol, updatedRow);
                case "4":
                    int updatedCol = PlayerCurrentCol - 1;
                    if (updatedCol < 0)
                        updatedCol = MapSize[size] - 1;
                    return CheckIfWumpasDead(updatedCol, PlayerCurrentRow);
                case "3":
                    updatedCol = PlayerCurrentCol + 1;
                    if (updatedCol < MapSize[size] - 1)
                        updatedCol = 0;
                    return CheckIfWumpasDead(updatedCol, PlayerCurrentRow);
                default:
                    return CheckIfWumpasDead(PlayerCurrentCol, PlayerCurrentRow);
            }

        }
        public static int Move(string size)
        {
            string direction = Console.ReadLine();
            switch (direction)
            {
                case "1":
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                    int updatedRow = PlayerCurrentRow - 1;
                    if (updatedRow < 0)
                        updatedRow = MapSize[size] - 1;
                    newMap[PlayerCurrentCol, updatedRow].Player = true;
                    PlayerCurrentRow = updatedRow;
                    return CheckIfPlayerIsDead(size);
                case "2":
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                    updatedRow = PlayerCurrentRow + 1;
                    if (updatedRow > MapSize[size] - 1)
                        updatedRow = 0;
                    newMap[PlayerCurrentCol, updatedRow].Player = true;
                    PlayerCurrentRow = updatedRow;
                    return CheckIfPlayerIsDead(size);
                case "4":
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                    int updatedCol = PlayerCurrentCol - 1;
                    if (updatedCol < 0)
                        updatedCol = MapSize[size] - 1;
                    newMap[updatedCol, PlayerCurrentRow].Player = true;
                    PlayerCurrentCol = updatedCol;
                    return CheckIfPlayerIsDead(size);
                case "3":
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                    updatedCol = PlayerCurrentCol + 1;
                    if (updatedCol > MapSize[size] - 1)
                        updatedCol = 0;
                    newMap[updatedCol, PlayerCurrentRow].Player = true;
                    PlayerCurrentCol = updatedCol;
                    return CheckIfPlayerIsDead(size);
                default:
                    return CheckIfPlayerIsDead(size);
            }
        }
        
        public static int CheckIfPlayerIsDead(string size)
        {
            if (newMap[PlayerCurrentCol, PlayerCurrentRow].Wampus == true)
                return WumpusEatsYou();
            else if (newMap[PlayerCurrentCol, PlayerCurrentRow].Pit == true)
                return FallInToPit();
            else if (newMap[PlayerCurrentCol, PlayerCurrentRow].Bats == true)
                return BatsCarry(size);
            else
                return 0;
        }
        public static int CheckIfWumpasDead(int updateCol, int updatedRow)
        {
            if (newMap[updateCol, updatedRow].Wampus == true)
            {
                Console.WriteLine("You shot Wumpas dead in the head, you win!!");
                return 100;
            }
            else
            {
                Console.WriteLine("You missed Wumpus...");
                return WumpusEatsYou();
            }
        }
        public static int WumpusEatsYou()
        {
            Console.WriteLine("You got eaten by Wumpas!");
            return 99;
        }
        public static int FallInToPit()
        {
            Console.WriteLine("You fell into a pit of acid and disolved!");
            return 99;
        }
        public static int BatsCarry(string size)
        {
            int batCarryChance = Loc.Next(4);
            if (batCarryChance == 2)
            {
                newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                int updatedCol = Loc.Next(MapSize[size]);
                int updatedRow = Loc.Next(MapSize[size]);
                newMap[updatedCol, updatedRow].Player = true;
                return CheckIfPlayerIsDead(size);
            }
            else
                return 0;
        }
        public static string PlayerCurrentLoc()
        {
            return PlayerLocation = $"[{PlayerCurrentCol},{PlayerCurrentRow}]";
        }


    }

}

﻿using System;
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

        public static List<string> MapInfo = new List<string>();

        public static List<string> VisitedPointOfInterest = new List<string>();

        public static Queue<string> VisitedTrace = new Queue<string>();

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
                    newMap[j,i] = new Cavern(j, i);
            }            
        }
        public static void assignWumpus(string size)
        {
            int col = Loc.Next(MapSize[size]);
            int row = Loc.Next(MapSize[size]);

            newMap[col, row].Wumpus = true;
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
                newMap[col, row].VisitedTrace = true;
                VisitedTrace.Enqueue($"[{newMap[col, row].Column},{newMap[col, row].Row}]");
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
        public static void addINFO(string size)
        {
            for (int i = 0; i < MapSize[size]; i++)
            {
                for (int j = 0; j < MapSize[size]; j++)
                {
                    if (newMap[j, i].Wumpus == true)
                        MapInfo.Add($"Wumpus: [{newMap[j, i].Column},{newMap[j, i].Row}]");
                    if (newMap[j, i].Blood == true)
                        MapInfo.Add($"Blood: {newMap[j, i].Column},{newMap[j, i].Row}");
                    if (newMap[j, i].Draft == true)
                        MapInfo.Add($"Draft: {newMap[j, i].Column},{newMap[j, i].Row}");
                    if (newMap[j, i].Pit == true)
                        MapInfo.Add($"Pit: [{newMap[j, i].Column},{newMap[j, i].Row}]");
                    if (newMap[j, i].Bats == true)
                        MapInfo.Add($"Bats: [{newMap[j, i].Column},{newMap[j, i].Row}]");
                    if (newMap[j, i].Player == true)
                        MapInfo.Add($"Player: [{newMap[j, i].Column},{newMap[j, i].Row}]");
                }
            }
        }
        public static void Trace(string size)
        {
            Console.Clear();
            printTraceCaverns(size);
            foreach (var item in VisitedTrace)
                Console.Write(item);
            Console.WriteLine();
            Console.WriteLine();

        }
        public static void Note(string size)
        {
            if(VisitedPointOfInterest.Count==0)
                Console.WriteLine("No hints available");
            else
            {
                Console.Clear();
                printNoteCavern(size);
                VisitedPointOfInterest.Sort();
                foreach (var item in VisitedPointOfInterest)
                    Console.WriteLine(item);
                Console.WriteLine();
            }
        }
        public static void Cheat(string size)
        {
            Console.Clear();
            printCheatedCavern(size);
            MapInfo.Sort();
            foreach (var item in MapInfo)
                Console.WriteLine(item);
            Console.WriteLine();
        }
        public static void printCaverns(string size)
        {
            for (int i = 0; i < MapSize[size]; i++)
            {
                for (int j = 0; j < MapSize[size]; j++)
                {
                    if (newMap[j,i].Player==true)
                    {
                        Console.Write($"[{newMap[j, i].Column},{newMap[j, i].Row}]  ");
                    }
                    else
                        Console.Write($" {newMap[j, i].Column},{newMap[j, i].Row}   ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void printTraceCaverns(string size)
        {
            for (int i = 0; i < MapSize[size]; i++)
            {
                for (int j = 0; j < MapSize[size]; j++)
                {
                    if (newMap[j, i].VisitedTrace == true)
                    {
                        Console.Write($"[{newMap[j, i].Column},{newMap[j, i].Row}]  ");
                    }
                    else
                        Console.Write($" {newMap[j, i].Column},{newMap[j, i].Row}   ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void printNoteCavern(string size)
        {
            for (int i = 0; i < MapSize[size]; i++)
            {
                for (int j = 0; j < MapSize[size]; j++)
                {
                    if (newMap[j, i].VisitedHint == true)
                    {
                        if (newMap[j, i].Bats == true)
                            Console.Write($"#{newMap[j, i].Column},{newMap[j, i].Row}#  ");
                        if (newMap[j, i].Blood == true & newMap[j, i].Draft == true)
                            Console.Write($"^{newMap[j, i].Column},{newMap[j, i].Row}^  ");
                        else if (newMap[j, i].Blood == true)
                            Console.Write($"+{newMap[j, i].Column},{newMap[j, i].Row}+  ");
                        else if (newMap[j, i].Draft == true)
                            Console.Write($"-{newMap[j, i].Column},{newMap[j, i].Row}-  ");
                    }
                    else
                        Console.Write($" {newMap[j, i].Column},{newMap[j, i].Row}   ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void printCheatedCavern(string size)
        {
            for (int i = 0; i < MapSize[size]; i++)
            {
                for (int j = 0; j < MapSize[size]; j++)
                {
                    if (newMap[j, i].Player == true)
                        Console.Write($"[{newMap[j, i].Column},{newMap[j, i].Row}]  ");
                    else if(newMap[j, i].Wumpus == true)
                        Console.Write($"[{newMap[j, i].Column},{newMap[j, i].Row}]  ");
                    else if(newMap[j, i].Pit == true)
                        Console.Write($"[{newMap[j, i].Column},{newMap[j, i].Row}]  ");
                    else if(newMap[j, i].Bats == true)
                        Console.Write($"[{newMap[j, i].Column},{newMap[j, i].Row}]  ");
                    else
                        Console.Write($" {newMap[j, i].Column},{newMap[j, i].Row}   ");
                }
                Console.WriteLine();
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
            addINFO(size);
        }
        public static int Shoot(string size)
        {
            Console.WriteLine();
            string direction = Console.ReadLine();
            switch (direction)
            {
                case "1":
                    int updatedRow = PlayerCurrentRow - 1;
                    if (updatedRow < 0)
                        updatedRow = MapSize[size] - 1;
                    return CheckIfWumpusDead(PlayerCurrentCol, updatedRow);
                case "2":
                    updatedRow = PlayerCurrentRow + 1;
                    if (updatedRow > MapSize[size] - 1)
                        updatedRow = 0;
                    return CheckIfWumpusDead(PlayerCurrentCol, updatedRow);
                case "4":
                    int updatedCol = PlayerCurrentCol - 1;
                    if (updatedCol < 0)
                        updatedCol = MapSize[size] - 1;
                    return CheckIfWumpusDead(updatedCol, PlayerCurrentRow);
                case "3":
                    updatedCol = PlayerCurrentCol + 1;
                    if (updatedCol < MapSize[size] - 1)
                        updatedCol = 0;
                    return CheckIfWumpusDead(updatedCol, PlayerCurrentRow);
                default:
                    Game.Update();
                    return 0;
            }
        }
        public static int Move(string size)
        {
            string direction = Console.ReadLine();
            switch (direction)
            {
                case "1":
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Occupied = false;
                    int updatedRow = PlayerCurrentRow - 1;
                    if (updatedRow < 0)
                        updatedRow = MapSize[size] - 1;
                    newMap[PlayerCurrentCol, updatedRow].Player = true;
                    PlayerCurrentRow = updatedRow;
                    VisitedTrace.Enqueue($"[{newMap[PlayerCurrentCol, updatedRow].Column},{newMap[PlayerCurrentCol, updatedRow].Row}]");
                    newMap[PlayerCurrentCol, updatedRow].VisitedTrace = true;
                    return CheckIfCavernIsOccupied();
                case "2":
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Occupied = false;

                    updatedRow = PlayerCurrentRow + 1;
                    if (updatedRow > MapSize[size] - 1)
                        updatedRow = 0;
                    newMap[PlayerCurrentCol, updatedRow].Player = true;
                    PlayerCurrentRow = updatedRow;
                    VisitedTrace.Enqueue($"[{newMap[PlayerCurrentCol, updatedRow].Column},{newMap[PlayerCurrentCol, updatedRow].Row}]");
                    newMap[PlayerCurrentCol, updatedRow].VisitedTrace = true;
                    return CheckIfCavernIsOccupied();
                case "4":
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Occupied = false;

                    int updatedCol = PlayerCurrentCol - 1;
                    if (updatedCol < 0)
                        updatedCol = MapSize[size] - 1;
                    newMap[updatedCol, PlayerCurrentRow].Player = true;
                    PlayerCurrentCol = updatedCol;
                    VisitedTrace.Enqueue($"[{newMap[updatedCol, PlayerCurrentRow].Column},{newMap[updatedCol, PlayerCurrentRow].Row}]");
                    newMap[updatedCol, PlayerCurrentRow].VisitedTrace = true;
                    return CheckIfCavernIsOccupied();
                case "3":
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                    newMap[PlayerCurrentCol, PlayerCurrentRow].Occupied = false;

                    updatedCol = PlayerCurrentCol + 1;
                    if (updatedCol > MapSize[size] - 1)
                        updatedCol = 0;
                    newMap[updatedCol, PlayerCurrentRow].Player = true;
                    PlayerCurrentCol = updatedCol;
                    VisitedTrace.Enqueue($"[{newMap[updatedCol, PlayerCurrentRow].Column},{newMap[updatedCol, PlayerCurrentRow].Row}]");
                    newMap[updatedCol, PlayerCurrentRow].VisitedTrace = true;
                    return CheckIfCavernIsOccupied();
                default:
                    Game.Update();
                    return 0;
            }
        }
        
        public static int CheckIfCavernIsOccupied()
        {
            if (newMap[PlayerCurrentCol, PlayerCurrentRow].Wumpus == true)
                return WumpusEatsYou();
            else if (newMap[PlayerCurrentCol, PlayerCurrentRow].Pit == true)
                return FallInToPit();
            //else if (newMap[PlayerCurrentCol, PlayerCurrentRow].Bats == true)
            //    return CheckBats(size);
            else
                return 0;
        }
        public static int CheckIfWumpusDead(int updateCol, int updatedRow)
        {
            if (newMap[updateCol, updatedRow].Wumpus == true)
            {
                Console.Clear();
                Console.WriteLine("You shot Wumpus dead in the head, you win!!\n");
                return 100;
            }
            else
            {
                Console.Clear();
                
                return WumpusEatsYou();
            }
        }
        public static int WumpusEatsYou()
        {
            Console.Clear();
            if (newMap[PlayerCurrentCol,PlayerCurrentRow].Wumpus == true)
            {
                Console.WriteLine("You got eaten by Wumpus!\n");
                return 99;
            }
            else
            {
                Console.WriteLine("You missed Wumpus...");
                Console.WriteLine("You got eaten by Wumpus!\n");
                return 99;
            }

            
        }
        public static int FallInToPit()
        {
            Console.Clear();
            Console.WriteLine("You fell into a pit of acid and disolved!\n");
            return 99;
        }
        public static int BatsCarry(string size)
        {
            
            int batCarryChance = Loc.Next(2);
            if (batCarryChance == 1)
            {
                newMap[PlayerCurrentCol, PlayerCurrentRow].Player = false;
                int updatedCol;
                int updatedRow;
                do
                {
                    updatedCol = Loc.Next(MapSize[size]);
                    updatedRow = Loc.Next(MapSize[size]);
                } while (newMap[updatedCol,updatedRow].Occupied==true);
                
                newMap[updatedCol, updatedRow].Player = true;
                PlayerCurrentCol = updatedCol;
                PlayerCurrentRow = updatedRow;
                Console.Clear();
                printCaverns(size);
                Console.WriteLine("You walked in to a group of bats");
                Console.WriteLine($"you were in Cavern: {Game.PlayerPreviousLocation}");
                Console.WriteLine($"The bats carried you to cavern: {PlayerCurrentLoc()}\n");
                return CheckIfCavernIsOccupied();
            }
            else
                return 0;
        }
        public static string PlayerCurrentLoc()
        {
            return PlayerLocation = $"[{PlayerCurrentCol},{PlayerCurrentRow}]";
        }
        public static string CheckBloodAndDraft()
        {
            if (newMap[PlayerCurrentCol, PlayerCurrentRow].Draft == true & newMap[PlayerCurrentCol, PlayerCurrentRow].Blood == true)
            {
                if (newMap[PlayerCurrentCol, PlayerCurrentRow].VisitedHint != true)
                {
                    VisitedPointOfInterest.Add($"[{newMap[PlayerCurrentCol, PlayerCurrentRow].Column},{newMap[PlayerCurrentCol, PlayerCurrentRow].Row}]: Draft");
                    VisitedPointOfInterest.Add($"[{newMap[PlayerCurrentCol, PlayerCurrentRow].Column},{newMap[PlayerCurrentCol, PlayerCurrentRow].Row}]: Blood");
                    newMap[PlayerCurrentCol, PlayerCurrentRow].VisitedHint = true;
                }
                return "\nYou detected both blood and draft in this cavern";
            }
            else
                return null;
        }
        public static string CheckBlood()
        {
            if (newMap[PlayerCurrentCol, PlayerCurrentRow].Blood == true)
            {
                if(newMap[PlayerCurrentCol, PlayerCurrentRow].VisitedHint != true)
                {
                    VisitedPointOfInterest.Add($"[{newMap[PlayerCurrentCol, PlayerCurrentRow].Column},{newMap[PlayerCurrentCol, PlayerCurrentRow].Row}]: Blood");
                    newMap[PlayerCurrentCol, PlayerCurrentRow].VisitedHint = true;
                }
                return "You see blood in this cavern";

            }
            else
                return null;
        }
        
        public static string CheckDraft()
        {
            if (newMap[PlayerCurrentCol, PlayerCurrentRow].Draft == true)
            {
                if (newMap[PlayerCurrentCol, PlayerCurrentRow].VisitedHint != true)
                {
                    VisitedPointOfInterest.Add($"[{newMap[PlayerCurrentCol, PlayerCurrentRow].Column},{newMap[PlayerCurrentCol, PlayerCurrentRow].Row}]: Draft");
                    newMap[PlayerCurrentCol, PlayerCurrentRow].VisitedHint = true;
                }
                return "\nYou detected draft in this cavern";
            }
            else
                return null;
        }
        public static int CheckBats(string size)
        {
            if (newMap[PlayerCurrentCol, PlayerCurrentRow].Bats == true)
            {
                if (newMap[PlayerCurrentCol, PlayerCurrentRow].VisitedHint != true)
                {
                    VisitedPointOfInterest.Add($"[{newMap[PlayerCurrentCol, PlayerCurrentRow].Column},{newMap[PlayerCurrentCol, PlayerCurrentRow].Row}]: Bats");
                    newMap[PlayerCurrentCol, PlayerCurrentRow].VisitedHint = true;
                }
                Console.WriteLine("You walked in to a group of bats");
                return BatsCarry(size);
            }
            else
                return 0;
        }


    }

}

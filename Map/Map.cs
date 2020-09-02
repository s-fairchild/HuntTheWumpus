using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusMap
{
    public class Map
    {
        public int[][] caveMap { get; } = new int[][] {new int[]{ 1,  5,  4},new int[]{ 0,  7,  2}, new int[] { 1, 9, 3 }, new int[] { 2, 11, 4 },
                 new int[] { 3, 13, 0 }, new int[] { 0, 14, 6 }, new int[] { 5, 16, 7 }, new int[] { 1, 6, 8 },
                 new int[] { 7, 9, 17 }, new int[] { 2, 8, 10 }, new int[] { 9, 11, 18 }, new int[] { 10, 3, 12 },
                 new int[] { 19, 11, 13 }, new int[] { 14, 12, 4 }, new int[] { 13, 5, 15 }, new int[] { 14, 19, 16 },
                 new int[] { 6, 15, 17 }, new int[] { 16, 8, 18 }, new int[] { 10, 17, 19 }, new int[] { 12, 15, 18 }};

        public void DrawMap()
        {
            for (int i = 0, count = 0, j = 0; count < caveMap.Length * 3; i++, count++)
            {
                if (i == 3)
                {
                    i = 0;
                    Console.WriteLine("Next Element");
                }
                Console.Write($"{caveMap[j][i]},");
                if (i == 0 && j != 19)
                    j++;
            }
        }
    }
}

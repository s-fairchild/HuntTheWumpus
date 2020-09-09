using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest1
{
    class Map
    {
        // Example read only jagged array read only property
        public int[][] Caves { get; } = {
            new int[] { 1, 2, 5, 8 },
            new int[] { 3, 2, 4, 12 },
            new int[] { 5, 4, 1, 6 },
            new int[] { 15, 14, 6, 16 },
            new int[] { 17, 7, 8, 6 },
            new int[] { 18, 9, 8, 10 },
            new int[] { 19, 11, 10, 12 },
            new int[] { 20, 13, 12, 14 },
        };

            public void MakeCaves() // Creates and populates a multidimensional array in a 5 long 4 deep grid pattern.
            {
                int[,] Cave3 = new int[5, 4];

                for (int i = 0, j = 0, k = 0; k <= 20; i++, k++)
                {
                    if (j >= 4)
                        j = 0;
                    if (i >= 5)
                        i = 0;
                    while (j < 4)
                    {
                        Cave3[i, j] = j;
                        j++;
                    }
                }
                for (int i = 0, j = 0, k = 0; k <= 20; i++, k++)
                {
                    if (j >= 4)
                        j = 0;
                    if (i >= 5)
                        i = 0;
                    while (j < 4)
                    {
                        Console.WriteLine($"Caves: {Cave3[i, j]}");
                        j++;
                    }
                }
    }
}

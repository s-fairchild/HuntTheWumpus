using System;
using System.Collections.Generic;
using System.Text;

namespace GameTest1
{
    class Map
    {
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
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Menu();
            Map.Initialize(Game.size);
            Game.Play();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        class Drone
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public WeightCatagories MaxWeight { get; set; }
            public DroneStatus Status { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                return $"{ID}";
            }
        }
    }
}

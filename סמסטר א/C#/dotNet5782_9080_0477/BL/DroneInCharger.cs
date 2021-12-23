﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class DroneInCharger
    {
        public DroneInCharger(BO.Drone drone)
        {
            ID = drone.ID;
            BatteryStatus = drone.BatteryStatus;
        }
        public DroneInCharger()
        {
            ID = 0;
            BatteryStatus = -1;
        }
        public int ID { get; set; }
        public double BatteryStatus { get; set; }
        public double TimeInCharge { get; set; }
        public override string ToString()
        {
            return $"DroneInCharger: ID: {ID}, BatteryStatus: {BatteryStatus}";
        }
    }
}

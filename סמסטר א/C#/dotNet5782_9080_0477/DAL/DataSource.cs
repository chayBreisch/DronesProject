﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        internal class Config
        {
            static internal int DronesIndex = 0;
            static internal int StationIndex = 0;
            static internal int CustomerIndex = 0;
            static internal int ParcialIndex = 0;
        }
        internal static Drone[] Drones = new Drone[10];
        internal static Station[] Stations = new Station[5];
        internal static Customer[] Customers = new Customer[100];
        internal static Parcial[] Parcials = new Parcial[1000];

        public static void Initialize()
        {
            Random rand = new Random();
            for (int i = 0; i < 2; i++)
            {
                Stations[Config.StationIndex].ID = Config.StationIndex + 1;
                Stations[Config.StationIndex].Name = Config.StationIndex + 1;
                Stations[Config.StationIndex].Latitude = rand.Next();
                Stations[Config.StationIndex].Longitude = rand.Next();
                Stations[Config.StationIndex].ChargeSlots = rand.Next(300);
                Config.StationIndex++;
            }

            for (int i = 0; i < 5; i++)
            {
                Drones[Config.DronesIndex].ID = Config.DronesIndex + 1;
                Drones[Config.DronesIndex].Model = "MarvicAir2";
                Drones[Config.DronesIndex].MaxWeight = WeightCatagories.Heavy + i;
                Drones[Config.DronesIndex].Status = DroneStatus.Delivery + i;
                Drones[Config.DronesIndex].Battery = rand.Next(100);
                Config.DronesIndex++;
            }

            for (int i = 0; i < 10; i++)
            {
                Customers[Config.CustomerIndex].ID = Config.CustomerIndex + 1;
                Customers[Config.CustomerIndex].Name = $"customer{i}";
                Customers[Config.CustomerIndex].Phone = $"{rand.Next(111111111, 999999999)}";
                Customers[Config.CustomerIndex].Latitude = rand.Next();
                Customers[Config.CustomerIndex].Longitude = rand.Next();
                Config.CustomerIndex++;
            }

            for (int i = 0; i < 10; i++)
            {
                Parcials[Config.ParcialIndex].ID = Config.ParcialIndex + 1;
                Parcials[Config.ParcialIndex].SenderID = rand.Next() % Config.CustomerIndex;
                Parcials[Config.ParcialIndex].TargetID = rand.Next() % Config.StationIndex;
                Parcials[Config.ParcialIndex].Weight = (WeightCatagories)(rand.Next() %3);
                Parcials[Config.ParcialIndex].Priority = (Priorities)(rand.Next() % 3);
                Parcials[Config.ParcialIndex].Requested = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                Parcials[Config.ParcialIndex].DroneID = rand.Next() % Config.DronesIndex;
                Parcials[Config.ParcialIndex].Scheduled = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                Parcials[Config.ParcialIndex].PickedUp = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60));
                Parcials[Config.ParcialIndex].Delivered = new DateTime(rand.Next(12), rand.Next(24), rand.Next(30), rand.Next(60), rand.Next(60), rand.Next(60)); ;
                Config.ParcialIndex++;

            }


        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public struct DataSource
    {
        
        internal static List<Drone> drones = new List<Drone>();
        internal static List<Station> stations = new List<Station>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcial> parcials =  new List<Parcial>();
        internal static List<DroneCharge> droneChargers = new List<DroneCharge>();

        public static void Initialize()
        {
            Random rand = new Random();
            int r = rand.Next(2,5);
            for (int i = 0; i < r; i++)
            {
                Station station = new Station();
                station.ID = stations.Count + 1;
                station.Name = stations.Count + 1;
                station.Latitude = rand.Next();
                station.Longitude = rand.Next();
                station.ChargeSlots = rand.Next(300);
                stations.Add(station);
            }

            r = rand.Next(5, 10);
            for (int i = 0; i < r; i++)
            {
                Drone drone = new Drone();
                drone.ID = drones.Count + 1;
                drone.Model = "MarvicAir2";
                drone.MaxWeight = WeightCatagories.Heavy + i;
                drone.Status = DroneStatus.Delivery + i;
                drone.Battery = rand.Next(100);
                drones.Add(drone);
            }

            r = rand.Next(10, 15);
            for (int i = 0; i < r; i++)
            {
                Customer customer = new Customer();
                customer.ID = customers.Count + 1;
                customer.Name = $"customer{i}";
                customer.Phone = $"{rand.Next(111111111, 999999999)}";
                customer.Latitude = rand.Next();
                customer.Longitude = rand.Next();
                customers.Add(customer);
            }
            Console.WriteLine("length" + customers.Count);
            r = rand.Next(10, 15);
            for (int i = 0; i < r; i++)
            {
                Parcial parcel = new Parcial();
                parcel.ID = parcials.Count + 1;
                parcel.SenderID = rand.Next() % parcials.Count;
                parcel.TargetID = rand.Next() % parcials.Count;
                parcel.Weight = (WeightCatagories)(rand.Next() %3);
                parcel.Priority = (Priorities)(rand.Next() % 3);
                parcel.Requested = new DateTime();
                parcel.DroneID = rand.Next() % parcials.Count;
                parcel.Scheduled = new DateTime();
                parcel.PickedUp = new DateTime();
                parcel.Delivered = new DateTime();
                parcials.Add(parcel);

            }
        }

    }
}

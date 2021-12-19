﻿using DAL;
using IBL.BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    internal partial class BL
    {

        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdStation(int id, IDAL.IDal dalObject)
        {
            List<DO.Station> stations = dalObject.GetStationByList();
            if (stations.Any(s => s.ID == id))
                throw new NotUniqeID(id, typeof(DO.Station));
        }

        /// <summary>
        /// add a station to the bl
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="ChargeSlots"></param>
        public void addStation(int id, int name, LocationBL location, int ChargeSlots)
        {
            List<DroneCharge> droneChargers = dalObject.GetDroneCharge().Cast<DroneCharge>().ToList();
            droneChargers = droneChargers.FindAll(d => d.StationID == id);
            List<DroneInCharger> dronesInCharges = new List<DroneInCharger>();
            droneChargers.ForEach(d => dronesInCharges.Add(new DroneInCharger(getSpecificDroneBLFromList(d.DroneID))));
            IBL.BO.Station station = new IBL.BO.Station(id, name, ChargeSlots, new LocationBL(location.Longitude, location.Latitude), dronesInCharges);
            AddStationToDal(id, name, location, ChargeSlots);
        }

        /// <summary>
        /// add a station to the dal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="ChargeSlots"></param>
        public void AddStationToDal(int id, int name, LocationBL location, int ChargeSlots)
        {
            DO.Station station = new DO.Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = location.Longitude;
            station.Latitude = location.Latitude;
            station.ChargeSlots = ChargeSlots;
            dalObject.AddStation(station);
        }

        /// <summary>
        /// return all the stations from the dal converted to bl
        /// </summary>
        /// <returns>List<StationBL></returns>
        public List<IBL.BO.Station> GetStationsBL()
        {

            IEnumerable<DO.Station> stations = dalObject.GetStation();
            List<IBL.BO.Station> stations1 = new List<IBL.BO.Station>();
            foreach (var station in stations)
            {
                stations1.Add(convertDalStationToBl(station));
            }
            return stations1;
        }

        /// <summary>
        /// returns a specific station by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Station</returns>
        public IBL.BO.Station GetSpecificStationBL(int id)
        {
            try
            {
                return convertDalStationToBl(dalObject.getStationById(s => s.ID == id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(DO.Station));

            }
        }

        /// <summary>
        /// returns the station with empty chargers
        /// </summary>
        /// <returns>List<StationBL></returns>
        public List<IBL.BO.Station> getStationWithEmptyChargers()
        {
            IEnumerable<DO.Station> stations = dalObject.GetStation();
            List<IBL.BO.Station> stations1 = new List<IBL.BO.Station>();
            foreach (var station in stations)
            {
                if (checkStationIfEmptyChargers(station))
                    stations1.Add(convertDalStationToBl(station));
            }
            return stations1;
        }

        /// <summary>
        /// returns if the station has empty chargers
        /// </summary>
        /// <param name="station"></param>
        /// <returns>bool</returns>
        public bool checkStationIfEmptyChargers(DO.Station station)
        {
            IEnumerable<DroneCharge> droneChargers = dalObject.GetDroneCharge();
            int numOfChargers = 0;
            foreach (var droneCharge in droneChargers)
            {
                if (station.ID == droneCharge.StationID)
                    numOfChargers++;
            }
            if (numOfChargers < station.ChargeSlots)
                return true;
            return false;
        }

        /// <summary>
        /// convert a parcel from dal to bl
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public IBL.BO.Station convertDalStationToBl(DO.Station s)
        {
            List<DroneCharge> droneChargers = dalObject.GetDroneCharge().Cast<DroneCharge>().ToList();
            droneChargers = droneChargers.FindAll(d => d.StationID == s.ID);
            List<DroneInCharger> dronesInCharges = new List<DroneInCharger>();
            droneChargers.ForEach(d => dronesInCharges.Add(new DroneInCharger(getSpecificDroneBLFromList(d.DroneID))));
            return new IBL.BO.Station(s.ID, s.Name, s.ChargeSlots, new LocationBL(s.Longitude, s.Latitude), dronesInCharges);
        }

        /// <summary>
        /// update the station
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="chargeSlots"></param>
        public void updateDataStation(int id, int name = 0, int chargeSlots = -1)
        {
            DO.Station station = dalObject.getStationById(s => s.ID == id);
            if (name != 0)
            {
                station.Name = name;
            }
            if (chargeSlots != -1)
            {
                station.ChargeSlots = chargeSlots;
            }
            dalObject.updateStation(station);
        }
    }
}

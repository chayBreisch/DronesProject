﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DAL;
using IBL.BO;

namespace BL
{
    internal partial class BL
    {
        /// <summary>
        /// check if the id is uniqe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dalObject"></param>
        public static void checkUniqeIdParcel(int id, IDAL.IDal dalObject)
        {
            List<IDAL.DO.Parcel> parcels = dalObject.GetParcelByList();
            if (parcels.Any(p => p.ID == id))
                throw new NotUniqeID(id, typeof(IDAL.DO.Parcel));
        }

        /// <summary>
        /// add a parcel to the bl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="Weight"></param>
        /// <param name="priority"></param>
        public void AddParcel(ulong sender, ulong target, int Weight, int priority)
        {
            IBL.BO.Parcel parcel = new IBL.BO.Parcel();
            parcel.Sender = new IBL.BO.Customer();
            parcel.Reciever = new IBL.BO.Customer();
            checkIfCustomerWithThisID(sender);
            checkIfCustomerWithThisID(target);
            parcel.Sender = GetSpecificCustomerBL(sender);
            parcel.Reciever = GetSpecificCustomerBL(target);
            parcel.Weight = (WeightCatagories)Weight;
            parcel.Priorities = (Priorities)priority;
            parcel.Requesed = DateTime.Now;
            parcel.Scheduled = null;
            parcel.PickedUp = null;
            parcel.Delivered = null;
            parcel.Drone = null;
            AddParcelToDal(sender, target, Weight, priority);

        }

        /// <summary>
        /// add a parcel to the dal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="target"></param>
        /// <param name="Weight"></param>
        /// <param name="priority"></param>
        public void AddParcelToDal(/*ulong id,*/ ulong sender, ulong target, int Weight, int priority)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
            parcel.ID = dalObject.lengthParcel() + 1;
            checkUniqeIdParcel(parcel.ID, dalObject);
            parcel.SenderID = sender;
            parcel.TargetID = target;
            parcel.Weight = (WeightCatagories)Weight;
            parcel.Priority = (Priorities)priority;
            dalObject.AddParcel(parcel);
        }

        /// <summary>
        /// return all the parcels from the dal converted to bl
        /// </summary>
        /// <returns>List<ParcelBL> </returns>
        public List<IBL.BO.Parcel> GetParcelsBL()
        {

            IEnumerable<IDAL.DO.Parcel> parcels = dalObject.GetParcel();
            List<IBL.BO.Parcel> parcel1 = new List<IBL.BO.Parcel>();
            foreach (var parcel in parcels)
            {
                parcel1.Add(convertDalToParcelBL(parcel));
            }
            return parcel1;
        }

        /// <summary>
        /// returns a specific parcel by id from dal converted to bl
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Parcel</returns>
        public IBL.BO.Parcel GetSpecificParcelBL(int id)
        {
            try
            {
                return convertDalToParcelBL(dalObject.getParcelById(p => p.ID == id));
            }
            catch (ArgumentNullException e)
            {
                throw new NotExistObjWithID(id, typeof(IDAL.DO.Parcel));
            }
        }

        /// <summary>
        /// return parcels that are not connected to a drone
        /// </summary>
        /// <returns> List<Parcel></returns>
        public List<IDAL.DO.Parcel> getParcelsWithoutoutDrone()
        {
            IEnumerable<IDAL.DO.Parcel> parcels = dalObject.GetParcel();
            List<IDAL.DO.Parcel> parcels1 = new List<IDAL.DO.Parcel>();
            foreach (var parcel in parcels)
            {
                if (parcel.DroneID == 0)
                {
                    parcels1.Add(parcel);
                }
            }
            return parcels1;
        }

        /// <summary>
        /// convert a parcel from dal to bl
        /// </summary>
        /// <param name="p"></param>
        /// <returns>ParcelBL</returns>
        public IBL.BO.Parcel convertDalToParcelBL(IDAL.DO.Parcel p)
        {
            IBL.BO.Customer sender = convertDalCustomerToBl(dalObject.getCustomerById(c => c.ID == p.SenderID));
            IBL.BO.Customer target = convertDalCustomerToBl(dalObject.getCustomerById(c => c.ID == p.TargetID));
            IBL.BO.Drone drone = new IBL.BO.Drone();
            if (p.DroneID != 0)
                drone = convertDalDroneToBl(dalObject.getDroneById(d => d.ID == p.DroneID));
            return new IBL.BO.Parcel
            {
                ID = p.ID,
                Sender = sender,
                Reciever = target,
                Weight = p.Weight,
                Priorities = p.Priority,
                PickedUp = p.PickedUp,
                Drone = drone,
                Requesed = p.Requested,
                Scheduled = p.Scheduled,
                Delivered = p.Delivered,
            };
        }

        /// <summary>
        /// returns the status of the parcel
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>ParcelStatus</returns>
        public ParcelStatus findParcelStatus(IDAL.DO.Parcel parcel)
        {
            if (parcel.Requested == null)
                return (ParcelStatus)0;
            else if (parcel.Scheduled == null)
                return (ParcelStatus)1;
            else if (parcel.PickedUp == null)
                return (ParcelStatus)2;
            return (ParcelStatus)3;
        }

    }

}

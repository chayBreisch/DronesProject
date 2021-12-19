﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelInDelivery
        {
            public ParcelInDelivery(IBL.BO.Parcel parcel, IDAL.IDal dalObject)
            {
                ID = parcel.ID;
                Weight = parcel.Weight;
                Priority = parcel.Priorities;
                isPickedUp = parcel.PickedUp != null;
                CustomerInDeliverySender = parcel.Sender;
                CustomerInDeliveryReciever = parcel.Reciever;
                DO.Customer customer = dalObject.getCustomerById(c => c.ID == parcel.Sender.ID);
                CollectLocation = new LocationBL(customer.Longitude, customer.Latitude);
                customer = dalObject.getCustomerById(c => c.ID == parcel.Reciever.ID);
                DeliveryDestinationLocation = new LocationBL(customer.Longitude, customer.Latitude);
                TransportDistance = BL.BL.distance(CollectLocation, DeliveryDestinationLocation);
            }
            public int ID { get; set; }
            public DO.WeightCatagories Weight { get; set; }
            public DO.Priorities Priority { get; set; }
            public bool isPickedUp { get; set; }
            public CustomerAtParcel CustomerInDeliverySender { get; set; }
            public CustomerAtParcel CustomerInDeliveryReciever { get; set; }
            public LocationBL CollectLocation { get; set; }
            public LocationBL DeliveryDestinationLocation { get; set; }
            public double TransportDistance { get; set; }

            public override string ToString()
            {
                return $"ParcelInDelivery: ID: {ID}, Weight: {Weight}, Priority: {Priority}, ParcelStatus: {isPickedUp}" +
                    $", CollectLocation: {CollectLocation}, CollectLocation: {CollectLocation}, DeliveryDestinationLocation: {DeliveryDestinationLocation}" +
                    $"TransportDistance: {TransportDistance}";
            }
        }
    }
}
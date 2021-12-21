﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL1
{
    /// <summary>
    /// Interaction logic for Station.xaml
    /// </summary>
    public partial class Station : Window
    {
        StationList StationList;
        BlApi.Bl blStation;
        BO.Station stationBL;
        /// <summary>
        /// constructor add statoin
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="droneList"></param>
        public Station(BlApi.Bl bl, StationList stationList)
        {
            StationList = stationList;
            blStation = bl;
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            actions.Visibility = Visibility.Hidden;
            addStation.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// constructor actions
        /// </summary>
        /// <param name="bl"></param>
        /// <param name="station"></param>
        /// <param name="stationList"></param>
        public Station(BlApi.Bl bl, BO.Station station, StationList stationList)
        {
            InitializeComponent();
            actions.Visibility = Visibility.Visible;
            addStation.Visibility = Visibility.Hidden;
            WindowStyle = WindowStyle.None;
            StationList = stationList;
            blStation = bl;
            stationBL = station;
            idstation.Text = stationBL.ID.ToString();
            nameStation.DataContext = stationBL;
            ChargeSlotsStation.DataContext = stationBL;
            LocationStation.Text = $"{stationBL.Location.Latitude}, {stationBL.Location.Longitude}";
        }

        //######################################################
        //add station
        //######################################################

        /// <summary>
        /// add station to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 


        private int getID()
        {
            try
            {
                return int.Parse(idStation.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("id");
            }
        }


        private BO.LocationBL getLocation()
        {
            try
            {
                return new BO.LocationBL(int.Parse(longitudeStation.Text), int.Parse(latitudeStation.Text));
            }
            catch (Exception e)
            {
                throw new InValidInput("location");
            }
        }

        private int getModel() 
        {

            if (latitudeStation.Text == "")
               throw new InValidInput("model");
            return int.Parse(latitudeStation.Text);
        }

        private int getchargeSlots()
        {
            try
            {
                return int.Parse(chargeSlotsStation.Text);
            }
            catch (Exception e)
            {
                throw new InValidInput("chargeSlots");
            }
        }

        private void Button_ClickAddStation(object sender, RoutedEventArgs e)
        {
            
           
            try
            {
                blStation.addStation(getID(), getModel(), getLocation(), getchargeSlots());
                MessageBox.Show("you added succefuly");
                StationList.Show();
                Close();
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
            }


        }


        //######################################################
        //actions
        //######################################################

        /// <summary>
        /// update the drone model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_ClickStation(object sender, RoutedEventArgs e)
        {
            stationBL = blStation.updateDataStation(stationBL.ID, int.Parse(nameStation.Text), int.Parse(ChargeSlotsStation.Text));
        }

        /// <summary>
        /// close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ClickClose(object sender, RoutedEventArgs e)
        {
            StationList.Show();
            Close();
        }

        private void Button_ClickReset(object sender, RoutedEventArgs e)
        {
            idStation.Text = null;
            chargeSlotsStation.Text = null;
            nameStationToAdd.Text = null;
            longitudeStation.Text = null;
            latitudeStation.Text = null;
        }
    }
}

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
    /// Interaction logic for FirstWindow.xaml
    /// </summary>
    public partial class FirstWindow : Window
    {
        BlApi.Bl BLObject;
        public FirstWindow(BlApi.Bl Blobject)
        {
            InitializeComponent();
            BLObject = Blobject;
        }

        private void Button_ClickNewCustomer(object sender, RoutedEventArgs e)
        {
            new NewCustomerWindow(BLObject);
        }

        private void Button_ClickWorker(object sender, RoutedEventArgs e)
        {

        }
    }
}

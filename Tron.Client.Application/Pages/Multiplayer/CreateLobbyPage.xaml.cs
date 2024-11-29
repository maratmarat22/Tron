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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tron.Client.Application.Pages.Multiplayer
{
    /// <summary>
    /// Interaction logic for CreateLobbyPage.xaml
    /// </summary>
    public partial class CreateLobbyPage : Page
    {
        public CreateLobbyPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }
        
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                GoBack();
            }
        }

        private void GoBack()
        {
            NavigationService.GoBack();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChatClient.ServiceChat;


namespace ChatClient
{
   
    public partial class MainWindow : Window
    {


        public ServiceChatClient client;
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
        }

    }
}

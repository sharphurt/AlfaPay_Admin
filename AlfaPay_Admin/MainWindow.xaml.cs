using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace AlfaPay_Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _allowDirectNavigation;
        private NavigatingCancelEventArgs _navArgs;
        private Duration _duration = new Duration(TimeSpan.FromSeconds(1));
        private double _oldHeight = 0;


        public MainWindow()
        {
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
            if (!Application.Current.Properties.Contains("AccessToken"))
                Application.Current.Properties.Add("AccessToken", "");
            if (!Application.Current.Properties.Contains("LoggedInUser"))
                Application.Current.Properties.Add("LoggedInUser", null);
        }

        private void MainWindow_OnLoaded(object sender, EventArgs args)
        {
            MinWidth = Width;
            MinHeight = Height;
        }
    }
}
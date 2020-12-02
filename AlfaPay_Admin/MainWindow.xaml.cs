using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using AlfaPay_Admin.WindowPage;

namespace AlfaPay_Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            // TODO: Delete after debugging
            //  new TestWindow().Show();
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, EventArgs args)
        {
            MinWidth = Width;
            MinHeight = Height;
        }
        
    }
}
using System;
using System.Windows.Input;

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
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, EventArgs args)
        {
            MinWidth = Width;
            MinHeight = Height;
        }
        
    }
}
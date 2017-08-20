using System.ComponentModel;
using System.Windows;


namespace BgetWpf.View
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }

        private void SettingWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void SaveEvent(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using AriaNet;
using BgetWpf.Controller;
using BgetWpf.View;

namespace BgetWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer AriaInfoRefresher;
        private AriaManager AriaManager;

        public MainWindow()
        {
            InitializeComponent();

            AriaManager = Properties.Settings.Default.UseExternalAria 
                ? new AriaManager(Properties.Settings.Default.ExternalRpc) 
                : new AriaManager();
            
            // Register the aria refresh timer
            AriaInfoRefresher = new DispatcherTimer()
            {
                Interval = new TimeSpan(0,0,0,1)
            };

            AriaInfoRefresher.Tick += AriaInfo_OnRefresh;
            AriaInfoRefresher.Start();
        }

        private void AddTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addTaskWindow = new AddTaskWindow();
            addTaskWindow.Show();
        }

        private void PauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void StartTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void RemoveTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void SettingButton_OnClick(object sender, RoutedEventArgs e)
        {
            var settingWindow = new SettingWindow();
            settingWindow.ShowDialog();
        }

        private void AboutButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private async void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            // Stop Aria2
            var ariaRunner = new AriaRunner();
            await ariaRunner.Stop();
        }

        private async void AriaInfo_OnRefresh(object sender, EventArgs e)
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Text.RegularExpressions;
using BgetWpf.Controller;
using BgetWpf.View;

namespace BgetWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // From: https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf
        private void InputValidate_NumOnly(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
    }
}

using System;
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
using BgetCore.User.UserResult;
using BgetCore.Video;
using BgetWpf.Model;

namespace BgetWpf.View
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private async void AddTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }


        private void AddTaskWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var addTaskButtonTitle = new BgetAddTaskBinding { ButtonContent = "Add task" };
            AddTaskButton.DataContext = addTaskButtonTitle;
        }
    }
}

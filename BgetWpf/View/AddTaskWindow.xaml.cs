using System.Windows;
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

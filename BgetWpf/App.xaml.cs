using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BgetWpf.Controller;

namespace BgetWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Initialize configurations
            InitialConfig.InitConfig();

            // Set up Aria2
            var ariaRunner = new AriaRunner();
            await ariaRunner.Start();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);


        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            BgetWpf.Properties.Settings.Default.Save();
        }
    }
}

using Dashboard_Admin;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using static Dashboard_Admin.Loginpage;

namespace DashboardAdmin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string FilePath = "RememberMe.json";
        public App()
        {
          
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window windowToOpen;

            if (File.Exists(FilePath))
            {
                string jsonContent = File.ReadAllText(FilePath);
                var settings = JsonSerializer.Deserialize<Settings>(jsonContent);

                if (settings != null && settings.RememberMe)
                {
                    // Open the main dashboard window if RememberMe is true
                    windowToOpen = new MainWindow();
                }
                else
                {
                    // Open the login page if RememberMe is false
                    windowToOpen = new Loginpage();
                }
            }
            else
            {
                // Open the login page if the file does not exist
                windowToOpen = new Loginpage();
            }

            // Set the window to open as the main window
            MainWindow = windowToOpen;
            windowToOpen.Show();
        }
    }

}

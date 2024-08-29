using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TriggerWPFUI.MVVM.ViewModels.Windows;
using TriggerWPFUI.Services;
using TriggerWPFUI.Views.Windows;
using Wpf.Ui;

namespace TriggerWPFUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? _host;

        public App()
        {
            InitializeHost();
        }

        private void InitializeHost()
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();
            builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IPageService, PageService>();

                // GetExecutingAssembly
                var assembly = Assembly.GetExecutingAssembly();
                // Add Window
                var windowTypes = assembly.GetTypes()
                    .Where(t => t.Name.EndsWith("Window") && t.IsClass && !t.IsAbstract);
                foreach (var windowType in windowTypes)
                {
                    services.AddSingleton(windowType);
                }
                // Add View
                var viewTypes = assembly.GetTypes()
                    .Where(t => t.Name.EndsWith("Page") && t.IsClass && !t.IsAbstract);
                foreach (var viewType in viewTypes)
                {
                    services.AddSingleton(viewType);
                }
                // Add ViewModel
                var viewModelTypes = assembly.GetTypes()
                    .Where(t => t.Name.EndsWith("ViewModel") && t.IsClass && !t.IsAbstract);
                foreach (var viewModelType in viewModelTypes)
                {
                    services.AddSingleton(viewModelType);
                }

            });

            _host = builder.Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                if (_host != null)
                {
                    await _host.StartAsync();

                    var mainWindow = _host.Services.GetRequiredService<MainWindow>();
                    var mainWindowViewModel = _host.Services.GetRequiredService<MainWindowViewModel>();
                    mainWindow.DataContext = mainWindowViewModel;
                    mainWindow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            try
            {
                if (_host != null)
                {
                    await _host.StopAsync();
                    _host.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during shutdown: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            base.OnExit(e);
        }

    }

}

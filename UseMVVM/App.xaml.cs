using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using UseMVVM.Services;

namespace UseMVVM
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesignMode { get; set; } = true;

        private IHost _host;

        public IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IsDesignMode = false;
            var host = Host;
            await host.StartAsync().ConfigureAwait(false);
            host.Dispose();
            _host = null;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            var host = Host;
            host.StopAsync().ConfigureAwait(false);
        }

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<DataService>();
        }

        public static string CurrentDirectory => IsDesignMode == true ?
            Path.GetDirectoryName(GetSourceCodePath()): 
            Environment.CurrentDirectory;

        public static string GetSourceCodePath([CallerFilePath]string path = null) => path;
    }
}

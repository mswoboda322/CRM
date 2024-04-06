﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text;
using System.Windows;
using Application;
using Infrastructure;

namespace CRM;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public IServiceProvider ServiceProvider { get; private set; }
    public IConfiguration Configuration { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);

        Configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        ServiceProvider = serviceCollection.BuildServiceProvider();
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddInfrastructureLayer(Configuration);
        serviceCollection.AddApplicationLayer();
        serviceCollection.AddTransient(typeof(MainWindow));
    }

}


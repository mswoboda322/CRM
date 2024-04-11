using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text;
using System.Windows;
using Application;
using Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Windows.Threading;
using Domain.Entities;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using CRM.Windows;
using CRM.Windows.EditWindows;
using CRM.Windows.CreateWindows;

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
        base.OnStartup(e);
    }

    private void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.AddInfrastructureLayer(Configuration);
        serviceCollection.AddApplicationLayer();
        serviceCollection.AddSingleton(typeof(MainWindow));
        serviceCollection.AddTransient(typeof(ContractorsWidnow));
        serviceCollection.AddTransient(typeof(MenuWindow));
        serviceCollection.AddTransient(typeof(TasksWindow));
        serviceCollection.AddTransient(typeof(UsersWindow));
        serviceCollection.AddTransient(typeof(EditUserWindow));
        serviceCollection.AddTransient(typeof(EditTaskWindow));
        serviceCollection.AddTransient(typeof(EditContractorWindow));
        serviceCollection.AddTransient(typeof(CreateUserWindow));
    }

}


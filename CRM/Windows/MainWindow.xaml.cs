using Application.Features.Users;
using CRM.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace CRM;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //private readonly IUserService _userService;
    private readonly ILogger<MainWindow> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ContractorsWidnow _contractorsWidnow;
    private readonly MenuWindow _menuWindow;
    public MainWindow(ILoggerFactory loggerFactory,
                      IServiceProvider serviceProvider,
                      ContractorsWidnow contractorsWidnow,
                      MenuWindow menuWindow)
    {
        //_userService = userService;
        _logger = loggerFactory.CreateLogger<MainWindow>();
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _contractorsWidnow = contractorsWidnow;
        _menuWindow = menuWindow;
    }

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);

        _logger.LogInformation("MainWindow rendered");
    }

    private async void GetUser(object sender, RoutedEventArgs e)
    {
        var _service = _serviceProvider.GetService<IUserService>();

        try
        {
            var user = await _service.GetAsync(1);
            App.Current.Properties["CurrentUser"] = user;
        }
        catch (Exception ex) 
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btn_Login(object sender, RoutedEventArgs e)
    {
        _menuWindow.Show();
        this.Hide();
    }
}
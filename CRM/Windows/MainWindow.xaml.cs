using Application.Features.Users;
using System.Windows;

namespace CRM;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IUserService _userService;
    public MainWindow(IUserService userService)
    {
        _userService = userService;
        InitializeComponent();
    }

    private async void GetUser(object sender, RoutedEventArgs e)
    {
        var user = await _userService.Get(1);
        Test.Content = user.Email;
        App.Current.Properties["CurrentUser"] = user;
        
    }
}
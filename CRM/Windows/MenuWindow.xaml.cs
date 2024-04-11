using Microsoft.Extensions.DependencyInjection;
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

namespace CRM.Windows;
/// <summary>
/// Interaction logic for MenuWindow.xaml
/// </summary>
public partial class MenuWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ContractorsWidnow _contractorsWidnow;
    private readonly TasksWindow _tasksWindow;
    private readonly UsersWindow _usersWindow;

    public MenuWindow(IServiceProvider serviceProvider,
                     ContractorsWidnow contractorsWidnow,
                     TasksWindow tasksWindow,
                     UsersWindow usersWindow)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _contractorsWidnow = contractorsWidnow;
        _tasksWindow = tasksWindow;
        _usersWindow = usersWindow;
    }

    private void btn_Contractors(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Wkrótce");
    }

    private void btn_Tasks(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Wkrótce");
    }

    private void btn_Users(object sender, RoutedEventArgs e)
    {
        var usersWindow = _serviceProvider.GetService<UsersWindow>();
        usersWindow.Show();
        this.Hide();
    }

    private void btn_Logout(object sender, RoutedEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.Show();
        this.Hide();
    }
}

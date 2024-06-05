using Application.Features.Users;
using Application.Features.Users.DTOs;
using CRM.Windows.CreateWindows;
using CRM.Windows.EditWindows;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
/// Interaction logic for UsersWindow.xaml
/// </summary>
public partial class UsersWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    private readonly EditUserWindow _editUserWindow;
    public UsersWindow(IServiceProvider serviceProvider, EditUserWindow editUserWindow)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _editUserWindow = editUserWindow;

        var userService = _serviceProvider.GetService<IUserService>();
        var users = userService.List(true);
        foreach (var user in users)
        {
            UsersTable.ItemsSource = users;
        }
    }

    private void btn_Logout(object sender, RoutedEventArgs e)
    {
        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow.Show();
        this.Hide();
    }

    private void btn_Menu(object sender, RoutedEventArgs e)
    {
        var menuWindow = _serviceProvider.GetService<MenuWindow>();
        menuWindow.Show();
        this.Hide();
    }

    private void btn_EditUser(object sender, RoutedEventArgs e)
    {
        var userId = (UsersTable.SelectedItem as UserDTO)?.Id;
        if (userId is null)
        {
            MessageBox.Show("Musisz wybrać użytkownika");
            return;
        }
        var editUserWindow = _serviceProvider.GetService<EditUserWindow>();
        editUserWindow.UserId = userId;
        editUserWindow.Show();
    }

    private void btn_Refresh(object sender, RoutedEventArgs e)
    {
        Refresh();
    }

    private void btn_CreateUser(object sender, RoutedEventArgs e)
    {
        var createUserWindow = _serviceProvider.GetService<CreateUserWindow>();
        createUserWindow.Show();
    }

    private async void btn_Delete(object sender, RoutedEventArgs e)
    {
        var userId = (UsersTable.SelectedItem as UserDTO)?.Id;
        if (userId is null)
        {
            MessageBox.Show("Musisz wybrać użytkownika");
            return;
        }
        var userService= _serviceProvider.GetService<IUserService>();
        await userService.DeleteAsync(userId.Value);
        MessageBox.Show("Użytkownik został usunięty");
        Refresh();    }

    private void Refresh()
    {
        var userService = _serviceProvider.GetService<IUserService>();
        var users = userService.List(true);
        foreach (var user in users)
        {
            UsersTable.ItemsSource = users;
        }
    }
}

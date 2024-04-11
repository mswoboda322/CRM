using Application.Features.Users.DTOs;
using Application.Features.Users;
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

namespace CRM.Windows.CreateWindows;
/// <summary>
/// Interaction logic for CreateUserWindow.xaml
/// </summary>
public partial class CreateUserWindow : Window
{
    private readonly IServiceProvider _serviceProvider;

    public CreateUserWindow(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();
    }

    private async void btn_Create(object sender, RoutedEventArgs e)
    {
        var userCreateDTO = new UserCreateDTO(Email.Text, FirsName.Text, LastName.Text, Password.Password);
        var userService = _serviceProvider.GetService<IUserService>();
        var newUserId = await userService.CreateAsync(userCreateDTO);
        MessageBox.Show($"Dodano nowego użytkownika o ID: {newUserId}");
    }

    private void btn_goBack(object sender, RoutedEventArgs e)
    {
        this.Hide();
    }
}

using Application.Features.Users;
using Application.Features.Users.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CRM.Windows.EditWindows;
/// <summary>
/// Interaction logic for EditUser.xaml
/// </summary>
public partial class EditUserWindow : Window
{
    private readonly IServiceProvider _serviceProvider;

    public long? UserId { get; set; }
    public EditUserWindow(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();
    }

    protected override void OnContentRendered(EventArgs e)
    {
        var userService = _serviceProvider.GetService<IUserService>();
        var user = userService.Get(UserId.Value);
        lbl_Title.Content = $"Edycja użytkownika ID: {user.Id}";
        Email.Text = user.Email;
        FirsName.Text = user.FirstName;
        LastName.Text = user.LastName;
        base.OnContentRendered(e);
    }

    private async void btn_SaveChanges(object sender, RoutedEventArgs e)
    {
        var userUpdateDTO = new UserUpdateDTO(UserId!.Value, Email.Text, FirsName.Text, LastName.Text);
        var userService = _serviceProvider.GetService<IUserService>();
        var isSuccess = await userService.UpdateAsync(userUpdateDTO);
        if (isSuccess)
            MessageBox.Show("Dane zmieniono pomyślnie");

    }

    private void btn_goBack(object sender, RoutedEventArgs e)
    {
        this.Hide();
    }
}

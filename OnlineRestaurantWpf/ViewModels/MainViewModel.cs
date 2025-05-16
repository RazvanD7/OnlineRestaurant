using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineRestaurantWpf.Models;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Windows; // For App.ServiceProvider

namespace OnlineRestaurantWpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject? _currentViewModel;

        [ObservableProperty]
        private string? _loggedInUserDisplayName;

        [ObservableProperty]
        private bool _isUserLoggedIn;

        private readonly LoginViewModel _loginViewModel;
        private readonly RegisterViewModel _registerViewModel;
        private readonly ClientMenuViewModel _clientMenuViewModel; // Added
                                                                   // private readonly AdminDashboardViewModel _adminDashboardViewModel; 


        public MainViewModel(LoginViewModel loginViewModel,
                             RegisterViewModel registerViewModel,
                             ClientMenuViewModel clientMenuViewModel /*, AdminDashboardViewModel adminVM */)
        {
            _loginViewModel = loginViewModel;
            _loginViewModel.LoginSuccessful += OnLoginSuccessful;
            _loginViewModel.NavigateToRegisterRequested += OnNavigateToRegisterRequested;

            _registerViewModel = registerViewModel;
            _registerViewModel.RegistrationSuccessful += OnRegistrationSuccessful;
            _registerViewModel.NavigateBackToLoginRequested += OnNavigateBackToLoginRequested;

            _clientMenuViewModel = clientMenuViewModel; // Store injected instance
                                                        // _adminDashboardViewModel = adminVM;

            CurrentViewModel = _loginViewModel;
            IsUserLoggedIn = false;
        }

        private void OnLoginSuccessful(User user)
        {
            LoggedInUserDisplayName = $"{user.FirstName} {user.LastName} ({user.Role})";
            IsUserLoggedIn = true;

            switch (user.Role?.ToLower())
            {
                case "admin":
                case "employee":
                    // CurrentViewModel = _adminDashboardViewModel; 
                    // _adminDashboardViewModel.LoadAdminDataCommand.Execute(null); 
                    // For now, fallback to login if Admin/Employee views are not ready
                    System.Windows.MessageBox.Show("Admin/Employee dashboard not yet implemented. Logging out.", "Not Implemented", MessageBoxButton.OK, MessageBoxImage.Information);
                    Logout();
                    break;
                case "client":
                    CurrentViewModel = _clientMenuViewModel;
                    _clientMenuViewModel.LoadMenuDataCommand.Execute(null); // Load menu data
                    break;
                default:
                    CurrentViewModel = _loginViewModel;
                    break;
            }
        }

        [RelayCommand]
        private void Logout()
        {
            LoggedInUserDisplayName = null;
            IsUserLoggedIn = false;
            CurrentViewModel = _loginViewModel;
        }

        private void OnNavigateToRegisterRequested()
        {
            CurrentViewModel = _registerViewModel;
        }

        private void OnRegistrationSuccessful()
        {
            CurrentViewModel = _loginViewModel;
        }

        private void OnNavigateBackToLoginRequested()
        {
            CurrentViewModel = _loginViewModel;
        }
    }
}
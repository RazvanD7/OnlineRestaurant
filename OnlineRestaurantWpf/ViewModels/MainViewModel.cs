using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineRestaurantWpf.Models;

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
        private readonly ClientMenuViewModel _clientMenuViewModel;

        public MainViewModel(LoginViewModel loginViewModel,
                             RegisterViewModel registerViewModel,
                             ClientMenuViewModel clientMenuViewModel)
        {
            _loginViewModel = loginViewModel;
            _loginViewModel.LoginSuccessful += OnLoginSuccessful;
            _loginViewModel.NavigateToRegisterRequested += OnNavigateToRegisterRequested;
            _loginViewModel.NavigateToClientViewRequested += OnNavigateToClientViewRequested;

            _registerViewModel = registerViewModel;
            _registerViewModel.RegistrationSuccessful += OnRegistrationSuccessful;
            _registerViewModel.NavigateBackToLoginRequested += OnNavigateBackToLoginRequested;

            _clientMenuViewModel = clientMenuViewModel;

            CurrentViewModel = _loginViewModel;
            IsUserLoggedIn = false;
        }

        private void OnLoginSuccessful(User user)
        {
            LoggedInUserDisplayName = $"{user.FirstName} {user.LastName} ({user.Role})";
            IsUserLoggedIn = true;
            CurrentViewModel = _clientMenuViewModel;
            _clientMenuViewModel.SetUserRole(user.Role);
            _clientMenuViewModel.LoadMenuDataCommand.Execute(null);
      
        }

        [RelayCommand]
        private void Logout()
        {
            LoggedInUserDisplayName = null;
            IsUserLoggedIn = false;
            _clientMenuViewModel.SetUserRole(null);
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

        private void OnNavigateToClientViewRequested()
        {
            LoggedInUserDisplayName = "No User";
            IsUserLoggedIn = true;
            CurrentViewModel = _clientMenuViewModel;
            _clientMenuViewModel.SetUserRole(null);
            _clientMenuViewModel.LoadMenuDataCommand.Execute(null);
        }
    }
}
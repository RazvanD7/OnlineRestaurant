using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineRestaurantWpf.BusinessLogicLayer;
using OnlineRestaurantWpf.Models;
using System;
using System.Security;
using System.Threading.Tasks;

namespace OnlineRestaurantWpf.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly UserBLL _userBLL;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _email;

        public SecureString? SecurePassword { get; set; }

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        private bool _isLoginInProgress;


        public event Action<User>? LoginSuccessful;
        public event Action? NavigateToRegisterRequested;
        public event Action? NavigateToClientViewRequested;


        public LoginViewModel(UserBLL userBLL)
        {
            _userBLL = userBLL;
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Email) &&
                   (SecurePassword != null && SecurePassword.Length > 0) &&
                   !IsLoginInProgress;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task LoginAsync()
        {
            if (SecurePassword == null)
            {
                ErrorMessage = "Password is required.";
                return;
            }

            IsLoginInProgress = true;
            ErrorMessage = null;

            IntPtr bstr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(SecurePassword);
            string passwordPlainText = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bstr);
            System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(bstr);

            try
            {
                var user = await _userBLL.AuthenticateUserAsync(Email!, passwordPlainText);
                if (user != null)
                {
                    LoginSuccessful?.Invoke(user);
                }
                else
                {
                    ErrorMessage = "Invalid email or password.";
                }
            }
            catch (Exception)
            {
                ErrorMessage = "An error occurred during login. Please try again.";
            }
            finally
            {
                IsLoginInProgress = false;
                SecurePassword?.Clear();
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        [RelayCommand]
        private void NavigateToRegister()
        {
            NavigateToRegisterRequested?.Invoke();
        }

        [RelayCommand]
        private void NavigateToClientView()
        {
            NavigateToClientViewRequested?.Invoke();
        }
    }
}
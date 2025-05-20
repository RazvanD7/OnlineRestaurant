using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineRestaurantWpf.BusinessLogicLayer;
using OnlineRestaurantWpf.Models;
using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows; // For MessageBox

namespace OnlineRestaurantWpf.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly UserBLL _userBLL;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        private string? _firstName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        private string? _lastName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
        private string? _email;

        [ObservableProperty]
        private string? _phone;

        [ObservableProperty]
        private string? _address;

        public SecureString? SecurePassword { get; set; }
        public SecureString? SecureConfirmPassword { get; set; }

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        private bool _isRegistrationInProgress;

        public event Action? RegistrationSuccessful;
        public event Action? NavigateBackToLoginRequested;

        public RegisterViewModel(UserBLL userBLL)
        {
            _userBLL = userBLL;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (SecurePassword == null || SecureConfirmPassword == null)
            {
                ErrorMessage = "Password and confirmation are required.";
                return;
            }

            IntPtr bstrPass = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(SecurePassword);
            string passwordPlainText = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bstrPass);
            System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(bstrPass);

            IntPtr bstrConfirmPass = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(SecureConfirmPassword);
            string confirmPasswordPlainText = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(bstrConfirmPass);
            System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(bstrConfirmPass);

            if (passwordPlainText != confirmPasswordPlainText)
            {
                ErrorMessage = "Passwords do not match.";
                passwordPlainText = string.Empty;
                confirmPasswordPlainText = string.Empty;
                SecurePassword.Clear();
                SecureConfirmPassword.Clear();
                RegisterCommand.NotifyCanExecuteChanged();
                return;
            }

            IsRegistrationInProgress = true;
            ErrorMessage = null;

            try
            {
                var newUser = new User
                {
                    FirstName = this.FirstName!,
                    LastName = this.LastName!,
                    Email = this.Email!,
                    Phone = this.Phone,
                    Address = this.Address,
                    Role = "Client"
                };

                await _userBLL.RegisterUserAsync(newUser, passwordPlainText);
                MessageBox.Show("Registration successful! Please log in.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                RegistrationSuccessful?.Invoke();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex is InvalidOperationException || ex is ArgumentException 
                    ? ex.Message 
                    : "An unexpected error occurred during registration.";
            }
            finally
            {
                IsRegistrationInProgress = false;
                passwordPlainText = string.Empty;
                confirmPasswordPlainText = string.Empty;
                SecurePassword?.Clear();
                SecureConfirmPassword?.Clear();
                RegisterCommand.NotifyCanExecuteChanged();
            }
        }

        [RelayCommand]
        private void NavigateBackToLogin()  
        {
            NavigateBackToLoginRequested?.Invoke();
        }
    }
}
using System.Windows.Controls;
using OnlineRestaurantWpf.ViewModels;

namespace OnlineRestaurantWpf.Views
{
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void PasswordBoxRegister_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext is RegisterViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.SecurePassword = passwordBox.SecurePassword;
            }
        }

        private void ConfirmPasswordBoxRegister_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext is RegisterViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.SecureConfirmPassword = passwordBox.SecurePassword;
            }
        }
    }
}
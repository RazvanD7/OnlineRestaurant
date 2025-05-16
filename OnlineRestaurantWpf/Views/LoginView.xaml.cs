using System.Windows.Controls;
using OnlineRestaurantWpf.ViewModels;

namespace OnlineRestaurantWpf.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBoxControl_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.DataContext is LoginViewModel viewModel && sender is PasswordBox passwordBox)
            {
                viewModel.SecurePassword = passwordBox.SecurePassword;
            }
        }
    }
}
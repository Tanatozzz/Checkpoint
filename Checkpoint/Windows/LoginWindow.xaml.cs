using System.Windows;
using System.Windows.Input;
using Checkpoint.API;
using Checkpoint.Windows;

namespace Checkpoint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txtLogin.Text;
            string password = passwordBox.Password;

            // Проверка на пустой ввод данных
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
                return;
            }

            // Проверка длины пароля и логина
            if (username.Length > 16 || password.Length > 16)
            {
                MessageBox.Show("Логин и пароль должны содержать не более 16 символов.");
                return;
            }

            HttpQuery loginManager = new HttpQuery();
            bool loginResult = await loginManager.Login(username, password);

            if (loginResult)
            {
                // Вход выполнен успешно
                MessageBox.Show("Вход выполнен успешно!");
                var mainWindow = new MainMenu();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // Ошибка аутентификации
                MessageBox.Show("Неверные учетные данные!");
            }
        }

        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogin.Text) && txtLogin.Text.Length > 0)
                textLogin.Visibility = Visibility.Collapsed;
            else
                textLogin.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtLogin.Focus();
        }
    }
}

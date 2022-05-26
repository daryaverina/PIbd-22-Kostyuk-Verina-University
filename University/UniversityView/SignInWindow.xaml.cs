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
using UniversityContracts.ViewModels;
using UniversityContracts.BusinessLogicsContracts;
using Unity;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        private readonly ICustomerLogic _logic;
        public SignInWindow(ICustomerLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void ToSignUp_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<SignUpWindow>();
            form.ShowDialog();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var customers = _logic.Read(null);
            CustomerViewModel _customer = null;

            foreach (var hw in customers)
            {
                //MessageBox.Show(hw.Login);
                if (hw.Login == TextBoxLogin.Text && hw.Password == TextBoxPassword.Text)
                {
                    _customer = hw;
                }
            }

            if (_customer != null)
            {
                App.Customer = _customer;
                var form = App.Container.Resolve<MainWindow>();
                Close();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неверно введён логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}

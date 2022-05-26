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
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.ViewModels;
using Unity;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для SubjectWindow.xaml
    /// </summary>
    public partial class SubjectWindow : Window
    {
        private readonly ISubjectLogic _logic;
        private readonly ICustomerLogic _customerLogic;
        public int Id { set { id = value; } }
        private int? id;

        public SubjectWindow(ISubjectLogic logic, ICustomerLogic customerLogic)
        {
            InitializeComponent();
            _logic = logic;
            _customerLogic = customerLogic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logic.CreateOrUpdate(new SubjectBindingModel
                {
                    Id = id,
                    SubjectName = TextBoxName.Text,
                    HoursAmount = Convert.ToInt32(TextBoxHours.Text),
                    CustomerID = (int)App.Customer.Id,
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (id != null)
            {
                var lunch = _logic.Read(new SubjectBindingModel
                {
                    Id = id
                })[0];

                TextBoxName.Text = lunch.SubjectName;
            }
        }
        
    }
}

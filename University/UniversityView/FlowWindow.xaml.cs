using System;
using System.Windows;
using UniversityBusinessLogic.BusinessLogics;
using UniversityContracts.BindingModels;
using Unity;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для FlowWindow.xaml
    /// </summary>
    public partial class FlowWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        FlowLogic _logic;
        public int _customerId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        public FlowWindow(FlowLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)

            {
                try
                {
                    var view = _logic.Read(new FlowBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        fName.Text = view.Faculty;
                        Course_number.Text = view.NumberOfCourse.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _logic.CreateOrUpdate(new FlowBindingModel
                {
                    Id = id,
                    Flow_name = flowName.Text,
                    Faculty = fName.Text,
                    NumberOfCourse = Convert.ToInt32(Course_number.Text),
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }
}
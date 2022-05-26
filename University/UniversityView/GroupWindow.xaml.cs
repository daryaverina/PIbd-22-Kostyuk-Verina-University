using System;
using System.Windows;
using UniversityBusinessLogic.BusinessLogics;
using UniversityContracts.BindingModels;
using Unity;

namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        GroupLogic _logic;
        public int _customerId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        public GroupWindow(GroupLogic logic)
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
                    var view = _logic.Read(new GroupBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        Spec_name.Text = view.Speciality;
                        Sem_amount.Text = view.SemestersAmount.ToString();
                        Date_created.Text = view.DateCreated.ToShortDateString();
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
                _logic.CreateOrUpdate(new GroupBindingModel
                {
                    Id = id,
                    Speciality = Spec_name.Text,
                    SemestersAmount = Convert.ToInt32(Sem_amount.Text),
                    DateCreated = Convert.ToDateTime(Date_created.Text),
                    CustomerID = (int)App.Customer.Id,
                    //FlowId = (int)App.Customer.Id,
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

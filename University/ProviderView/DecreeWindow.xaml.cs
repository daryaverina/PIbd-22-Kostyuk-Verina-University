using System;
using System.Windows;
using UniversityBusinessLogic.BusinessLogics;
using UniversityContracts.BindingModels;
using Unity;
using UniversityContracts.ViewModels;
using UniversityContracts.BusinessLogicsContracts;
using System.Collections.Generic;

namespace ProviderView
{
    /// <summary>
    /// Логика взаимодействия для DecreeWindow.xaml
    /// </summary>
    public partial class DecreeWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IGroupLogic _logic;
        private readonly IFlowLogic _flowLogic;

        public int _customerId { get; set; }
        public int Id { set { id = value; } }
        private int? id;
        public DecreeWindow(IGroupLogic logic, IFlowLogic flowLogic)
        {
            InitializeComponent();
            _logic = logic;
            _flowLogic = flowLogic;
        }
        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            List<FlowViewModel> list = _flowLogic.Read(null);
            if (list != null)
            {

                ComboBoxFlow.DisplayMemberPath = "Flow_name";
                ComboBoxFlow.SelectedValuePath = "Id";
                ComboBoxFlow.ItemsSource = list;
                ComboBoxFlow.SelectedItem = null;
            }
            else MessageBox.Show("не видны данные ");
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
                        ComboBoxFlow.SelectedValue = view.FlowId;

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
                    FlowId = ((FlowViewModel)ComboBoxFlow.SelectedItem).Id,
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

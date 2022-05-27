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
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.StorageContracts;
using Unity;


namespace ProviderView
{
    /// <summary>
    /// Логика взаимодействия для LinkGroupDecree.xaml
    /// </summary>
    public partial class LinkGroupDecree : Window
    {
        private readonly IStudentLogic _studentLogic;
        private readonly IFlowLogic _flowLogic;
        public int Id { set { id = value; } }
        private int? id;
        public LinkGroupDecree(IStudentLogic studentLogic, IFlowLogic flowLogic)
        {
            InitializeComponent();
            _studentLogic = studentLogic;
            _flowLogic = flowLogic;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<FlowViewModel> list = _flowLogic.Read(null);
            if (list != null)
            {

                ComboBoxFlow.DisplayMemberPath = "Flow_name";
                ComboBoxFlow.SelectedValuePath = "Id";
                ComboBoxFlow.ItemsSource = list;
                ComboBoxFlow.SelectedItem = null;
            }
            /*ComboBoxFlow.ItemsSource = _flowLogic.Read(new FlowBindingModel
            {
                CustomerID = (int)App.Customer.Id
            });*/
            //ComboBoxFlow.SelectedItem = null;

            var liststudents = _studentLogic.Read(new StudentBindingModel
            {
                ProviderId = 1
            }); 
          //  var liststudents = _studentLogic.Read(null);
           // foreach (var stu in liststudents)
          //  {
            //    ListBoxStudents.Items.Add(stu);
          //  }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                Dictionary<int, string> studentFlow = new Dictionary<int, string>();
                foreach (var flo in ListBoxFlow.SelectedItems)
                {
                    var flow = (FlowViewModel)flo;
                    studentFlow.Add(flow.Id, flow.Flow_name);
                }

                StudentViewModel student = (StudentViewModel)ComboBoxStudent.SelectedItem;
                _studentLogic.CreateOrUpdate(new StudentBindingModel
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    PhoneNumber = student.PhoneNumber,
                    ProviderId = student.ProviderId,
                    DecreeStudents = studentFlow,
                    StudentFlows = studentFlow
                });
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
           
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

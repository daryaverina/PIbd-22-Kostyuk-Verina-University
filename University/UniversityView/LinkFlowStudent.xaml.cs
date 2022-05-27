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


namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для LinkFlowStudent.xaml
    /// </summary>
    public partial class LinkFlowStudent : Window
    {
        private readonly IStudentLogic _studentLogic;
        private readonly IFlowLogic _flowLogic;
        public int Id { set { id = value; } }
        private int? id;
        public LinkFlowStudent(IStudentLogic studentLogic, IFlowLogic flowLogic)
        {
            InitializeComponent();
            _studentLogic = studentLogic;
            _flowLogic = flowLogic;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            List<FlowViewModel> list = _flowLogic.Read(new FlowBindingModel
            {
                CustomerID = (int)App.Customer.Id
            });
            if (list != null)
            {

                ComboBoxFlow.DisplayMemberPath = "Flow_name";
                ComboBoxFlow.SelectedValuePath = "Id";
                ComboBoxFlow.ItemsSource = list;
                ComboBoxFlow.SelectedItem = null;
            }

            var liststudents = _studentLogic.Read(null);

            foreach (var stu in liststudents)
            {
                ListBoxStudents.Items.Add(stu);
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                Dictionary<int, string> studentFlow = new Dictionary<int, string>();
                foreach (var stu in ListBoxStudents.SelectedItems)
                {
                    var student= (StudentViewModel)stu;
                    studentFlow.Add(student.Id, student.FullName);
                }


                FlowViewModel flow = (FlowViewModel)ComboBoxFlow.SelectedItem;
                _flowLogic.CreateOrUpdate(new FlowBindingModel
                {
                    Id = flow.Id,
                    Flow_name = flow.Flow_name,
                    Faculty = flow.Faculty,
                    NumberOfCourse = flow.NumberOfCourse,
                    CustomerID = (int)App.Customer.Id,
                    StudentFlows = studentFlow
                });
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}

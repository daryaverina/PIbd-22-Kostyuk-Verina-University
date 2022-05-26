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
using Unity;


namespace UniversityView
{
    /// <summary>
    /// Логика взаимодействия для SubjectsWindow.xaml
    /// </summary>
    public partial class SubjectsWindow : Window
    {
        private readonly ISubjectLogic _logic;
        public SubjectsWindow(ISubjectLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var form = App.Container.Resolve<SubjectWindow>();
                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SubjectsWindow_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = _logic.Read(new SubjectBindingModel
                {
                    CustomerID = (int)App.Customer.Id
                });
                if (list != null)
                {
                    MessageBox.Show("лист не пуст");
                    DataGridSubjects.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            
        }
    }
}

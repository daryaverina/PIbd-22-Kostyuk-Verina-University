using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemSubjects_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<SubjectsWindow>();
            form.ShowDialog();
        }

        private void MenuItemFlows_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<FlowsWindow>();
            form.ShowDialog();
        }

        private void MenuItemGroups_Click(object sender, RoutedEventArgs e)
        {
            var form = App.Container.Resolve<GroupsWindow>();
            form.ShowDialog();
        }
    }
}

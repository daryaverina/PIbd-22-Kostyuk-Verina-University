using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UniversityBusinessLogic.BusinessLogics;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityContracts.BindingModels;
using UniversityDatabaseImplement.Implements;
using UniversityBusinessLogic.OfficePackage;
using Unity;
using Unity.Lifetime;
using System.Windows;

namespace UniversityView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static CustomerViewModel Customer { get; set; }
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var authorizationWindow = Container.Resolve<SignInWindow>();
            authorizationWindow.ShowDialog();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<ICustomerStorage, CustomerStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISubjectStorage, SubjectStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGroupStorage, GroupStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFlowStorage, FlowStorage>(new HierarchicalLifetimeManager());


            currentContainer.RegisterType<ICustomerLogic, CustomerLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISubjectLogic, SubjectLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IGroupLogic, GroupLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IFlowLogic, FlowLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}


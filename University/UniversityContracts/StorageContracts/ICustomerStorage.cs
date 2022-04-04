using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.StorageContracts
{
    public interface ICustomerStorage
    {
        List<CustomerViewModel> GetFullList();

        List<CustomerViewModel> GetFilteredList(CustomerBindingModel model);

        CustomerViewModel GetElement(CustomerBindingModel model);

        void Insert(CustomerBindingModel model);

        void Update(CustomerBindingModel model);

        void Delete(CustomerBindingModel model);
    }
}

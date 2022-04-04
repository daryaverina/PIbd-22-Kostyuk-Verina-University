using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.StorageContracts
{
    public interface IFlowStorage
    {
        List<FlowViewModel> GetFullList();

        List<FlowViewModel> GetFilteredList(FlowBindingModel model);

        FlowViewModel GetElement(FlowBindingModel model);

        void Insert(FlowBindingModel model);

        void Update(FlowBindingModel model);

        void Delete(FlowBindingModel model);
    }
}

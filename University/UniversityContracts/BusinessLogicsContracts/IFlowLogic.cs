using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.BusinessLogicsContracts
{
    public interface IFlowLogic
    {
        List<FlowViewModel> Read(FlowBindingModel model);
        void CreateOrUpdate(FlowBindingModel model);
        void Delete(FlowBindingModel model);
    }
}

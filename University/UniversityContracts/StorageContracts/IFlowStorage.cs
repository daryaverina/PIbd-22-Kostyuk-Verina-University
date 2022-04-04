using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

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

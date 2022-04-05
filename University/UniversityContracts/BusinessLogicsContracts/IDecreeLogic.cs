using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicsContracts
{
    public interface IDecreeLogic
    {
        List<DecreeViewModel> Read(DecreeBindingModel model);

        void CreateOrUpdate(DecreeBindingModel model);

        void Delete(DecreeBindingModel model);
    }
}

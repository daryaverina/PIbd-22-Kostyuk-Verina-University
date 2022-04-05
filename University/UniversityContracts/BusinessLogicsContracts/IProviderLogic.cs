using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicsContracts
{
    public interface IProviderLogic
    {
        List<ProviderViewModel> Read(ProviderBindingModel model);

        void CreateOrUpdate(ProviderBindingModel model);

        void Delete(ProviderBindingModel model);
    }
}

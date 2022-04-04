using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.StorageContracts
{
    public interface IProviderStorage
    {
        List<ProviderViewModel> GetFullList();

        List<ProviderViewModel> GetFilteredList(ProviderBindingModel model);

        ProviderViewModel GetElement(ProviderBindingModel model);

        void Insert(ProviderBindingModel model);

        void Update(ProviderBindingModel model);

        void Delete(ProviderBindingModel model);
    }
}

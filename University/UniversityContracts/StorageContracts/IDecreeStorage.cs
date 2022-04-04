using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.StorageContracts
{
    public interface IDecreeStorage
    {
        List<DecreeViewModel> GetFullList();

        List<DecreeViewModel> GetFilteredList(DecreeBindingModel model);

        DecreeViewModel GetElement(DecreeBindingModel model);

        void Insert(DecreeBindingModel model);

        void Update(DecreeBindingModel model);

        void Delete(DecreeBindingModel model);
    }
}

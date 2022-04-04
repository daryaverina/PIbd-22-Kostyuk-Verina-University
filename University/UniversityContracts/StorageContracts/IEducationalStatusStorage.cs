using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.StorageContracts
{
    public interface IEducationalStatusStorage
    {
        List<EducationalStatusViewModel> GetFullList();

        List<EducationalStatusViewModel> GetFilteredList(EducationalStatusBindingModel model);

        EducationalStatusViewModel GetElement(EducationalStatusBindingModel model);

        void Insert(EducationalStatusBindingModel model);

        void Update(EducationalStatusBindingModel model);

        void Delete(EducationalStatusBindingModel model);
    }
}

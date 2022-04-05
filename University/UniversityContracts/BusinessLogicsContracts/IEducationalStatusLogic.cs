using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicsContracts
{
    public interface IEducationalStatusLogic
    {
        List<EducationalStatusViewModel> Read(EducationalStatusBindingModel model);

        void CreateOrUpdate(EducationalStatusBindingModel model);

        void Delete(EducationalStatusBindingModel model);
    }
}

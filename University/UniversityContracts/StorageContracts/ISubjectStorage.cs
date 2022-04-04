using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.StorageContracts
{
    public interface ISubjectStorage
    {
        List<SubjectViewModel> GetFullList();

        List<SubjectViewModel> GetFilteredList(SubjectBindingModel model);

        SubjectViewModel GetElement(SubjectBindingModel model);

        void Insert(SubjectBindingModel model);

        void Update(SubjectBindingModel model);

        void Delete(SubjectBindingModel model);
    }
}

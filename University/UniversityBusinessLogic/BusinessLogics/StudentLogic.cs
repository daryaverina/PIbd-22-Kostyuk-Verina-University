using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IStudentStorage _studentStorage;

        public StudentLogic(IStudentStorage studentStorage)
        {
            _studentStorage = studentStorage;
        }

        public List<StudentViewModel> Read(StudentBindingModel model)
        {
            if (model == null)
            {
                return _studentStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<StudentViewModel> { _studentStorage.GetElement(model) };
            }

            return _studentStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel { FullName = model.FullName });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Есть студент(ка) с идентичными ФИО");
            }

            if (model.Id.HasValue)
            {
                _studentStorage.Update(model);
            }
            else
            {
                _studentStorage.Insert(model);
            }
        }

        public void Delete(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel { Id = model.Id });

            if (element == null)
            {
                throw new Exception("Студент(ка) не найден(а)");
            }

            _studentStorage.Delete(model);
        }
    }
}

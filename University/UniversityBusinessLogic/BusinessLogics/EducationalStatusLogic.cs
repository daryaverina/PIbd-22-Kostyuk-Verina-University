using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class EducationalStatusLogic : IEducationalStatusLogic
    {
        private readonly IEducationalStatusStorage _educationalStatusStorage;

        public EducationalStatusLogic(IEducationalStatusStorage educationalStatusStorage)
        {
            _educationalStatusStorage = educationalStatusStorage;
        }

        public List<EducationalStatusViewModel> Read(EducationalStatusBindingModel model)
        {
            if (model == null)
            {
                return _educationalStatusStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<EducationalStatusViewModel> { _educationalStatusStorage.GetElement(model) };
            }

            return _educationalStatusStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(EducationalStatusBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _educationalStatusStorage.Update(model);
            }
            else
            {
                _educationalStatusStorage.Insert(model);
            }
        }

        public void Delete(EducationalStatusBindingModel model)
        {
            var element = _educationalStatusStorage.GetElement(new EducationalStatusBindingModel { Id = model.Id });

            if (element == null)
            {
                throw new Exception("Статус обучения не найден");
            }

            _educationalStatusStorage.Delete(model);
        }
    }
}

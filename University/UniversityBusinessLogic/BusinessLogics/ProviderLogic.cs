using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class ProviderLogic : IProviderLogic
    {
        private readonly IProviderStorage _providerStorage;

        public ProviderLogic(IProviderStorage providerStorage)
        {
            _providerStorage = providerStorage;
        }

        public List<ProviderViewModel> Read(ProviderBindingModel model)
        {
            if (model == null)
            {
                return _providerStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<ProviderViewModel> { _providerStorage.GetElement(model) };
            }

            return _providerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ProviderBindingModel model)
        {
            var element = _providerStorage.GetElement(new ProviderBindingModel { Email = model.Email });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть поставщик с такой почтой");
            }

            if (model.Id.HasValue)
            {
                _providerStorage.Update(model);
            }
            else
            {
                _providerStorage.Insert(model);
            }
        }

        public void Delete(ProviderBindingModel model)
        {
            var element = _providerStorage.GetElement(new ProviderBindingModel { Id = model.Id });

            if (element == null)
            {
                throw new Exception("Поставщик не найден");
            }

            _providerStorage.Delete(model);
        }
    }
}
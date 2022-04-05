using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class DecreeLogic : IDecreeLogic
    {
        private readonly IDecreeStorage _decreeStorage;

        public DecreeLogic(IDecreeStorage decreeStorage)
        {
            _decreeStorage = decreeStorage;
        }

        public List<DecreeViewModel> Read(DecreeBindingModel model)
        {
            if (model == null)
            {
                return _decreeStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<DecreeViewModel> { _decreeStorage.GetElement(model) };
            }

            return _decreeStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(DecreeBindingModel model)
        {
            var element = _decreeStorage.GetElement(new DecreeBindingModel { DecreeNumber = model.DecreeNumber });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть приказ с таким номером");
            }

            if (model.Id.HasValue)
            {
                _decreeStorage.Update(model);
            }
            else
            {
                _decreeStorage.Insert(model);
            }
        }

        public void Delete(DecreeBindingModel model)
        {
            var element = _decreeStorage.GetElement(new DecreeBindingModel { Id = model.Id });

            if (element == null)
            {
                throw new Exception("Приказ не найден");
            }

            _decreeStorage.Delete(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class FlowLogic : IFlowLogic
    {
        private readonly IFlowStorage _flowStorage;
        public FlowLogic(IFlowStorage flowStorage)
        {
            _flowStorage = flowStorage;
        }
        public List<FlowViewModel> Read(FlowBindingModel model)
        {
            if (model == null)
            {
                return _flowStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<FlowViewModel> { _flowStorage.GetElement(model) };
            }
            return _flowStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(FlowBindingModel model)
        {
            var element = _flowStorage.GetElement(new FlowBindingModel
            {
                Flow_name = model.Flow_name
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть поток с таким названием");
            }
            if (model.Id.HasValue)
            {
                _flowStorage.Update(model);
            }
            else
            {
                _flowStorage.Insert(model);
            }
        }
        public void Delete(FlowBindingModel model)
        {
            var element = _flowStorage.GetElement(new FlowBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _flowStorage.Delete(model);
        }
    }
}
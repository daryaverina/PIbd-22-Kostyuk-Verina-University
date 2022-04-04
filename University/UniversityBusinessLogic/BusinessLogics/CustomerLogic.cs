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
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerStorage _customerStorage;
        public CustomerLogic(ICustomerStorage customerStorage)
        {
            _customerStorage = customerStorage;
        }
        public List<CustomerViewModel> Read(CustomerBindingModel model)
        {
            if (model == null)
            {
                return _customerStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CustomerViewModel> { _customerStorage.GetElement(model) };
            }
            return _customerStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(CustomerBindingModel model)
        {
            var element = _customerStorage.GetElement(new CustomerBindingModel
            {
                Login = model.Login
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть заказчик с таким логином");
            }
            if (model.Id.HasValue)
            {
                _customerStorage.Update(model);
            }
            else
            {
                _customerStorage.Insert(model);
            }
        }
        public void Delete(CustomerBindingModel model)
        {
            var element = _customerStorage.GetElement(new CustomerBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _customerStorage.Delete(model);
        }
    }
}
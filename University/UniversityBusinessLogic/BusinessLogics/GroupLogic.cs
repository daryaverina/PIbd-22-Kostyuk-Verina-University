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
    public class GroupLogic : IGroupLogic
    {
        private readonly IGroupStorage _groupStorage;
        public GroupLogic(IGroupStorage groupStorage)
        {
            _groupStorage = groupStorage;
        }
        public List<GroupViewModel> Read(GroupBindingModel model)
        {
            if (model == null)
            {
                return _groupStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<GroupViewModel> { _groupStorage.GetElement(model) };
            }
            return _groupStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(GroupBindingModel model)
        {
            var element = _groupStorage.GetElement(new GroupBindingModel
            {
                Speciality = model.Speciality
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть группа с таким названием");
            }
            if (model.Id.HasValue)
            {
                _groupStorage.Update(model);
            }
            else
            {
                _groupStorage.Insert(model);
            }
        }
        public void Delete(GroupBindingModel model)
        {
            var element = _groupStorage.GetElement(new GroupBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Удаляемый элемент не найден");
            }
            _groupStorage.Delete(model);
        }
    }
}
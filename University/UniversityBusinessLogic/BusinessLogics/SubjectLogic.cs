using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class SubjectLogic : ISubjectLogic
    {
        private readonly ISubjectStorage _subjectStorage;
        public SubjectLogic(ISubjectStorage subjectStorage)
        {
            _subjectStorage = subjectStorage;
        }
        public void CreateOrUpdate(SubjectBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _subjectStorage.Update(model);
            }
            else
            {
                _subjectStorage.Insert(model);
            }
        }

        public void Delete(SubjectBindingModel model)
        {
            var element = _subjectStorage.GetElement(new SubjectBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Дисциплина не найдена");
            }
            _subjectStorage.Delete(model);
        }

        public List<SubjectViewModel> Read(SubjectBindingModel model)
        {
            if (model == null)
            {
                return _subjectStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
               // throw new Exception(model.Id.ToString());
                 return new List<SubjectViewModel> { _subjectStorage.GetElement(model) };
            }
            return _subjectStorage.GetFilteredList(model);
        }
    }
}
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.BusinessLogicsContracts
{
    public interface ISubjectLogic
    {
        List<SubjectViewModel> Read(SubjectBindingModel model);
        void CreateOrUpdate(SubjectBindingModel model);
        void Delete(SubjectBindingModel model);
    }
}
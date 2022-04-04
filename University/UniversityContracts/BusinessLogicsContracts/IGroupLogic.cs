using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityContracts.BusinessLogicsContracts
{
    public interface IGroupLogic
    {
        List<GroupViewModel> Read(GroupBindingModel model);
        void CreateOrUpdate(GroupBindingModel model);
        void Delete(GroupBindingModel model);
    }
}
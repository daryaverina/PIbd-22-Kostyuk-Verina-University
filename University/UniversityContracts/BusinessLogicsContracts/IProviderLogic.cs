using System.Collections.Generic;
using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicsContracts
{
    public interface IProviderLogic
    {
        List<ProviderViewModel> Read(ProviderBindingModel model);

        // Регистрация и авторизация
        void CreateOrUpdate(ProviderBindingModel model);

        void Delete(ProviderBindingModel model);
    }
}

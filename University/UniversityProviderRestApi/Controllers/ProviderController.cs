using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UniversityProviderRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderLogic _providerLogic;

        public ProviderController(IProviderLogic providerLogic)
        {
            _providerLogic = providerLogic;
        }

        [HttpGet]
        public ProviderViewModel Login(string email, string password)
        {
            var list = _providerLogic.Read(new ProviderBindingModel
            {
                Email = email,
                Password = password
            });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(ProviderBindingModel model) => _providerLogic.CreateOrUpdate(model);

        //[HttpPost]
        //public void UpdateData(ProviderBindingModel model) => _providerLogic.CreateOrUpdate(model);
    }
}
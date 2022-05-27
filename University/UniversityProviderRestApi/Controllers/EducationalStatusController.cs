using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EducationalStatusController : ControllerBase
    {
        private readonly IEducationalStatusLogic _status;

        public EducationalStatusController(IEducationalStatusLogic status)
        {
            _status = status;
        }

        [HttpGet]
        public List<EducationalStatusViewModel> GetEducationalStatusList() => _status.Read(null)?.ToList();

        [HttpGet]
        public EducationalStatusViewModel GetEducationalStatus(int statusId) => _status.Read(new EducationalStatusBindingModel { Id = statusId })?[0];

        [HttpGet]
        public List<EducationalStatusViewModel> GetEducationalStatuss(int providerId) => _status.Read(new EducationalStatusBindingModel { ProviderId = providerId });

        [HttpPost]
        public void CreateOrUpdateEducationalStatus(EducationalStatusBindingModel model) => _status.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteEducationalStatus(EducationalStatusBindingModel model) => _status.Delete(model);
    }
}
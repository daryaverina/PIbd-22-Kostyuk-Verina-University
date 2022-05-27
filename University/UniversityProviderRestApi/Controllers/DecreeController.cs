using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DecreeController : ControllerBase
    {
        private readonly IDecreeLogic _decree;

        public DecreeController(IDecreeLogic decree)
        {
            _decree = decree;
        }

        [HttpGet]
        public List<DecreeViewModel> GetDecreeList() => _decree.Read(null)?.ToList();

        [HttpGet]
        public DecreeViewModel GetDecree(int decreeId) => _decree.Read(new DecreeBindingModel { Id = decreeId })?[0];

        [HttpGet]
        public List<DecreeViewModel> GetDecrees(int providerId) => _decree.Read(new DecreeBindingModel { ProviderId = providerId });

        [HttpPost]
        public void CreateOrUpdateDecree(DecreeBindingModel model) => _decree.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteDecree(DecreeBindingModel model) => _decree.Delete(model);
    }
}
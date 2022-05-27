using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLogic _student;

        public StudentController(IStudentLogic student)
        {
            _student = student;
        }

        [HttpGet]
        public List<StudentViewModel> GetStudentList() => _student.Read(null)?.ToList();

        [HttpGet]
        public StudentViewModel GetStudent(int studentId) => _student.Read(new StudentBindingModel { Id = studentId })?[0];

        [HttpGet]
        public List<StudentViewModel> GetStudents(int providerId) => _student.Read(new StudentBindingModel { ProviderId = providerId });

        [HttpPost]
        public void CreateOrUpdateStudent(StudentBindingModel model) => _student.CreateOrUpdate(model);

        [HttpPost]
        public void DeleteStudent(StudentBindingModel model) => _student.Delete(model);
    }
}
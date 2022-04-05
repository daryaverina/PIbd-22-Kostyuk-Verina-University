using UniversityContracts.BindingModels;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityDatabaseImplement.Implements
{
    public class StudentStorage : IStudentStorage
    {
        public List<StudentViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();

            return context.Students
            .Include(rec => rec.DecreeStudents)
            .ThenInclude(rec => rec.Decree)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<StudentViewModel> GetFilteredList(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();

            return context.Students
            .Include(rec => rec.DecreeStudents)
            .ThenInclude(rec => rec.Decree)
            .Where(rec => rec.FullName.Contains(model.FullName))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public StudentViewModel GetElement(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();

            var student = context.Students
            .Include(rec => rec.DecreeStudents)
            .ThenInclude(rec => rec.Decree)
            .ToList()
            .FirstOrDefault(rec => rec.FullName == model.FullName || rec.Id == model.Id);
            
            return student != null ? CreateModel(student) : null;
        }

        public void Insert(StudentBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.Students.Add(CreateModel(model, new Student(), context));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(StudentBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var student = context.Students.FirstOrDefault(rec => rec.Id == model.Id);
                if (student == null)
                {
                    throw new Exception("Студент(ка) не найден(а)");
                }

                CreateModel(model, student, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(StudentBindingModel model)
        {
            using var context = new UniversityDatabase();

            Student student = context.Students.FirstOrDefault(rec => rec.Id == model.Id);

            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Студент(ка) не найден(а)");
            }
        }

        private static Student CreateModel(StudentBindingModel model, Student student, UniversityDatabase context)
        {
            student.FullName = model.FullName;
            student.PhoneNumber = model.PhoneNumber;
            student.ProviderId = (int)model.ProviderId;

            if (model.Id.HasValue)
            {
                var decreeStudents = context.DecreeStudents.Where(rec => rec.StudentId == model.Id.Value).ToList();

                // Удалили те, которых нет в модели
                context.DecreeStudents.RemoveRange(decreeStudents.Where(rec => !model.DecreeStudents.ContainsKey(rec.DecreeId)).ToList());
                context.SaveChanges();
            }

            // Добавили новые
            foreach (var ds in model.DecreeStudents)
            {
                context.DecreeStudents.Add(new DecreeStudent
                {
                    StudentId = student.Id,
                    DecreeId = ds.Key,
                });
                context.SaveChanges();
            }

            return student;
        }

        private static StudentViewModel CreateModel(Student student)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                FullName = student.FullName,
                PhoneNumber = student.PhoneNumber,
                DecreeStudents = student.DecreeStudents.ToDictionary(recDS => recDS.DecreeId, recDS => recDS.Decree.DateOfCreation)
            };
        }
    }
}
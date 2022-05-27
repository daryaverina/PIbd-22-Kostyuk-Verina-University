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
                .Include(rec => rec.StudentFlows)
                    .ThenInclude(rec => rec.Flow)
                        .ThenInclude(rec => rec.Faculty)
                 .Include(rec => rec.StudentFlows)
                    .ThenInclude(rec => rec.Flow)
                        .ThenInclude(rec => rec.NumberOfCourse)
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
                .Include(rec => rec.StudentFlows)
                    .ThenInclude(rec => rec.Flow)
                        .ThenInclude(rec => rec.Faculty)
                 .Include(rec => rec.StudentFlows)
                    .ThenInclude(rec => rec.Flow)
                        .ThenInclude(rec => rec.NumberOfCourse)
                .Where(rec => rec.ProviderId == model.ProviderId)
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
                .Include(rec => rec.StudentFlows)
                    .ThenInclude(rec => rec.Flow)
                        .ThenInclude(rec => rec.Faculty)
                 .Include(rec => rec.StudentFlows)
                    .ThenInclude(rec => rec.Flow)
                        .ThenInclude(rec => rec.NumberOfCourse)
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
                Student student = new Student
                {
                    ProviderId = model.ProviderId,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                };
                context.Students.Add(student);
                context.SaveChanges();
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

        public void Update(StudentBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var student = context.Students.Include(rec => rec.ProviderId).FirstOrDefault(rec => rec.Id == model.Id);
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
                // затираем связи с приказами
                var studentDecrees = context.DecreeStudents.Where(rec => rec.StudentId == model.Id.Value).ToList();
                context.DecreeStudents.RemoveRange(studentDecrees.ToList());
                context.SaveChanges();

                // затираем связи с потоками
                var studentFlows = context.FlowStudents.Where(rec => rec.StudentId == model.Id.Value).ToList();
                context.FlowStudents.RemoveRange(studentFlows.ToList());
                context.SaveChanges();

                // затираем связи со статусами
                var studentStatuses = context.EducationalStatuses.Where(rec => rec.StudentId == model.Id.Value);
                foreach (var s in studentStatuses)
                {
                    s.Student = null;
                }
                context.SaveChanges();

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
            student.ProviderId = model.ProviderId;

            if (model.Id.HasValue)
            {
                var flowStudents = context.FlowStudents.Where(rec => rec.StudentId == model.Id.Value).ToList();

                // Удалили те, которых нет в модели
                context.FlowStudents.RemoveRange(flowStudents.Where(rec => !model.StudentFlows.ContainsKey(rec.FlowId)).ToList());
                context.SaveChanges();
            }

            // Добавили новые
            foreach (var sf in model.StudentFlows)
            {
                context.FlowStudents.Add(new FlowStudent
                {
                    StudentId = student.Id,
                    FlowId = sf.Key,
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
                StudentFlows = student.StudentFlows.ToDictionary(recDS => recDS.FlowId, recDS => recDS.Flow?.Faculty),
                ProviderId = student.ProviderId
            };
        }
    }
}
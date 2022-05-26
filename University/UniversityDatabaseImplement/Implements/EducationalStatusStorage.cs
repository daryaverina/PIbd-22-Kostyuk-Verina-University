using UniversityContracts.BindingModels;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityDatabaseImplement.Implements
{
    public class EducationalStatusStorage : IEducationalStatusStorage
    {
        public List<EducationalStatusViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();

            return context.EducationalStatuses
                .Include(rec => rec.ProviderId)
                .Select(CreateModel)
                .ToList();
        }

        // получение отчета по записям статуса обучения студентов
        // на указанном потоке за определенный период 
        public List<EducationalStatusViewModel> GetFilteredList(EducationalStatusBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();


            return context.EducationalStatuses
                .Include(rec => rec.Student)
                .Include(rec => rec.ProviderId)
                // сначала проверяем провайдера потом идем дальше
                // идем дальше: если есть ограничения по дате справа и слева сверяемся с ними
                // потом надо как-то выбирать те записи у которых студенты на указанном потоке
                .Where(rec => (rec.ProviderId == model.ProviderId ))
                 //       && (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateOfChange.Date >= model.DateFrom.Value.Date && rec.DateOfChange.Date <= model.DateTo.Value.Date)))
                .Select(CreateModel)
                .ToList();
        }

        public EducationalStatusViewModel GetElement(EducationalStatusBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();

            var status = context.EducationalStatuses
                .Include(rec => rec.ProviderId)
                .FirstOrDefault(rec => rec.Id == model.Id);

            return status != null ? CreateModel(status) : null;
        }

        public void Insert(EducationalStatusBindingModel model)
        {
            using var context = new UniversityDatabase();

            context.EducationalStatuses.Add(CreateModel(model, new EducationalStatus()));
            context.SaveChanges();
        }

        public void Update(EducationalStatusBindingModel model)
        {
            using var context = new UniversityDatabase();

            var status = context.EducationalStatuses.FirstOrDefault(rec => rec.Id == model.Id);
            if (status == null)
            {
                throw new Exception("Статус не найден");
            }

            CreateModel(model, status);
            context.SaveChanges();
        }

        public void Delete(EducationalStatusBindingModel model)
        {
            using var context = new UniversityDatabase();

            EducationalStatus status = context.EducationalStatuses.FirstOrDefault(rec => rec.Id == model.Id);

            if (status != null)
            {
                context.EducationalStatuses.Remove(status);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Статус не найден");
            }
        }

        private static EducationalStatus CreateModel(EducationalStatusBindingModel model, EducationalStatus status)
        {
            status.BStatus = model.BStatus;
            status.FStatus = model.FStatus;
            status.DateOfChange = model.DateOfChange;
            status.StudentId = model.StudentId;
            status.ProviderId = model.ProviderId;
            return status;
        }

        private static EducationalStatusViewModel CreateModel(EducationalStatus status)
        {
            return new EducationalStatusViewModel
            {
                Id = status.Id,
                BStatus = status.BStatus,
                FStatus = status.FStatus,
                DateOfChange = status.DateOfChange,
                StudentId = status.StudentId,
                ProviderId = status.ProviderId
            };
        }
    }
}
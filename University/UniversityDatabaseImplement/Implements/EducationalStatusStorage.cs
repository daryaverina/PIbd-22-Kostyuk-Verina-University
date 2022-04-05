using UniversityContracts.BindingModels;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
    public class EducationalStatusStorage : IEducationalStatusStorage
    {
        public List<EducationalStatusViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();

            return context.EducationalStatuses
            .Select(CreateModel)
            .ToList();
        }

        public List<EducationalStatusViewModel> GetFilteredList(EducationalStatusBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();

            return context.EducationalStatuses
            .Where(rec => rec.Id == model.Id)
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
            status.ProviderId = (int)model.ProviderId;
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
            };
        }
    }
}
using UniversityContracts.BindingModels;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityDatabaseImplement.Implements
{
    public class DecreeStorage : IDecreeStorage
    {
        public List<DecreeViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();

            return context.Decrees
            .Include(rec => rec.DecreeStudents)
            .ThenInclude(rec => rec.Student)
            .Include(rec => rec.DecreeGroups)
            .ThenInclude(rec => rec.Group)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<DecreeViewModel> GetFilteredList(DecreeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();

            // ПОПРАВИТЬ: фильтрацию после того как
            //// (бизнес логика) ПОПРАВИТЬ: добавить другое отличительное поле приказу (не номер) ИЛИ разделять номер и айди

            return context.Decrees
            .Include(rec => rec.DecreeStudents)
            .ThenInclude(rec => rec.Student)
            .Include(rec => rec.DecreeGroups)
            .ThenInclude(rec => rec.Group)
            .Where(rec => rec.Id.Equals(model.Id)) // вот тут
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public DecreeViewModel GetElement(DecreeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();

            var decree = context.Decrees
            .Include(rec => rec.DecreeStudents)
            .ThenInclude(rec => rec.Student)
            .Include(rec => rec.DecreeGroups)
            .ThenInclude(rec => rec.Group)
            .ToList()
            .FirstOrDefault(rec => rec.Id == model.Id); // тут тоже можно (см выше)

            return decree != null ? CreateModel(decree) : null;
        }

        public void Insert(DecreeBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.Decrees.Add(CreateModel(model, new Decree(), context));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(DecreeBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var decree = context.Decrees.FirstOrDefault(rec => rec.Id == model.Id);
                if (decree == null)
                {
                    throw new Exception("Приказ не найден");
                }

                CreateModel(model, decree, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(DecreeBindingModel model)
        {
            using var context = new UniversityDatabase();

            Decree decree = context.Decrees.FirstOrDefault(rec => rec.Id == model.Id);

            if (decree != null)
            {
                context.Decrees.Remove(decree);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Приказ не найден");
            }
        }

        // ПОПРАВИТЬ: создание и изменение записи приказа с возможностью выбора нескольких студентов из списка
        private static Decree CreateModel(DecreeBindingModel model, Decree decree, UniversityDatabase context)
        {
            decree.DateOfCreation = model.DateOfCreation;
            decree.ProviderId = (int)model.ProviderId;

            if (model.Id.HasValue)
            {
                var decreeGroups = context.DecreeGroups.Where(rec => rec.DecreeId == model.Id.Value).ToList();

                // Удалили те, которых нет в модели
                context.DecreeGroups.RemoveRange(decreeGroups.Where(rec => !model.DecreeGroups.ContainsKey(rec.GroupId)).ToList());
                context.SaveChanges();
            }

            // Добавили новые
            foreach (var dg in model.DecreeGroups)
            {
                context.DecreeGroups.Add(new DecreeGroup
                {
                    DecreeId = decree.Id,
                    GroupId = dg.Key,
                });
                context.SaveChanges();
            }

            return decree;
        }

        private static DecreeViewModel CreateModel(Decree decree)
        {
            return new DecreeViewModel
            {
                Id = decree.Id,
                DateOfCreation = decree.DateOfCreation,
                DecreeGroups = decree.DecreeGroups.ToDictionary(recDG => recDG.GroupId, recDG => recDG.Group?.Speciality)
            };
        }
    }
}
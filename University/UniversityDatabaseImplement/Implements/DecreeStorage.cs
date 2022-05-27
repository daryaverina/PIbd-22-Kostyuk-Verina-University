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
                .Include(rec => rec.DecreeGroups)
                    .ThenInclude(rec => rec.Group)
                        .ThenInclude(rec => rec.Speciality)
                .Include(rec => rec.DecreeGroups)
                    .ThenInclude(rec => rec.Group)
                        .ThenInclude(rec => rec.SemestersAmount)
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
       
            return context.Decrees
                .Include(rec => rec.DecreeGroups)
                    .ThenInclude(rec => rec.Group)
                        .ThenInclude(rec => rec.Speciality)
                .Include(rec => rec.DecreeGroups)
                    .ThenInclude(rec => rec.Group)
                        .ThenInclude(rec => rec.SemestersAmount)
                .Where(rec => rec.ProviderId == model.ProviderId)
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
                .Include(rec => rec.DecreeGroups)
                    .ThenInclude(rec => rec.Group)
                        .ThenInclude(rec => rec.Speciality)
                .Include(rec => rec.DecreeGroups)
                    .ThenInclude(rec => rec.Group)
                        .ThenInclude(rec => rec.SemestersAmount)
                .ToList()
                .FirstOrDefault(rec => rec.Id == model.Id || rec.DecreeNumber == model.DecreeNumber); 

            return decree != null ? CreateModel(decree) : null;
        }

        public void Insert(DecreeBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                Decree decree = new Decree
                {
                    ProviderId = model.ProviderId,
                    DateOfCreation = model.DateOfCreation,
                    DecreeNumber = model.DecreeNumber, 
                };

                context.Decrees.Add(decree);
                context.SaveChanges();
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

        public void Update(DecreeBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var decree = context.Decrees.Include(rec => rec.ProviderId).FirstOrDefault(rec => rec.Id == model.Id);
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

       // зачищать связи ?
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

        private static Decree CreateModel(DecreeBindingModel model, Decree decree, UniversityDatabase context)

        {
            decree.DecreeNumber = model.DecreeNumber;
            decree.DateOfCreation = model.DateOfCreation;
            decree.ProviderId = model.ProviderId;

            if (model.Id.HasValue)
            {
                var decreeStudents = context.DecreeStudents.Where(rec => rec.DecreeId == model.Id.Value).ToList();

                // Удалили те, которых нет в модели
                context.DecreeStudents.RemoveRange(decreeStudents.Where(rec => !model.DecreeStudents.ContainsKey(rec.StudentId)).ToList());
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

            foreach (var ds in model.DecreeStudents)
            {
                context.DecreeStudents.Add(new DecreeStudent
                {
                    DecreeId = decree.Id,
                    StudentId = ds.Key,
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
                DecreeStudents = decree.DecreeStudents.ToDictionary(recDG => recDG.StudentId, recDG => recDG.Student.FullName),
                ProviderId = decree.ProviderId
            };
        }
    }
}
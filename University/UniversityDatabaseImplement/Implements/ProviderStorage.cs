using Microsoft.EntityFrameworkCore;
using UniversityContracts.BindingModels;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
    public class ProviderStorage : IProviderStorage
    {
        public List<ProviderViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();

            return context.Providers
            .Include(rec => rec.Students)
            .Include(rec => rec.Decrees)
            .Include(rec => rec.EducationalStatuses)
            .Select(CreateModel)
            .ToList();
        }

        public List<ProviderViewModel> GetFilteredList(ProviderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();

            return context.Providers
            .Include(rec => rec.Students)
            .Include(rec => rec.Decrees)
            .Include(rec => rec.EducationalStatuses)
            .Where(rec => rec.Id == model.Id)
            .Select(CreateModel)
            .ToList();
        }

        public ProviderViewModel GetElement(ProviderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new UniversityDatabase();

            var provider = context.Providers
            .Include(rec => rec.Students)
            .Include(rec => rec.Decrees)
            .Include(rec => rec.EducationalStatuses)
            .FirstOrDefault(rec => rec.Login == model.Login || rec.Id == model.Id);

            return provider != null ? CreateModel(provider) : null;
        }

        public void Insert(ProviderBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.Providers.Add(CreateModel(model, new Provider()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(ProviderBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var provider = context.Providers.FirstOrDefault(rec => rec.Id == model.Id);
                if (provider == null)
                {
                    throw new Exception("Статус не найден");
                }

                CreateModel(model, provider);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }            
        }

        public void Delete(ProviderBindingModel model)
        {
            using var context = new UniversityDatabase();

            Provider provider = context.Providers.FirstOrDefault(rec => rec.Id == model.Id);

            if (provider != null)
            {
                context.Providers.Remove(provider);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Статус не найден");
            }
        }

        private static Provider CreateModel(ProviderBindingModel model, Provider provider)
        {
            provider.Login = model.Login;
            provider.Password = model.Password;
            provider.FullName = model.FullName;
            provider.Email = model.Email;
            provider.PhoneNumber = model.PhoneNumber;
            return provider;
        }

        private static ProviderViewModel CreateModel(Provider provider)
        {
            return new ProviderViewModel
            {
                Id = provider.Id,
                Login = provider.Login,
                Password = provider.Password,
                FullName = provider.FullName,
                Email = provider.Email,
                PhoneNumber = provider.PhoneNumber
            };
        }
    }
}
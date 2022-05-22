﻿using UniversityContracts.BindingModels;
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
                .Where(rec => rec.Email == model.Email && rec.Password == model.Password)
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
                .FirstOrDefault(rec => rec.Email == model.Email || rec.Id == model.Id);

            return provider != null ? CreateModel(provider) : null;
        }

        public void Insert(ProviderBindingModel model)
        {
            using var context = new UniversityDatabase();

            context.Providers.Add(CreateModel(model, new Provider()));
            context.SaveChanges();
        }

        public void Update(ProviderBindingModel model)
        {
            using var context = new UniversityDatabase();

            var provider = context.Providers.FirstOrDefault(rec => rec.Id == model.Id);
            if (provider == null)
            {
                throw new Exception("Поставщик не найден");
            }

            CreateModel(model, provider);
            context.SaveChanges();
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
                throw new Exception("Поставщик не найден");
            }
        }

        private static Provider CreateModel(ProviderBindingModel model, Provider provider)
        {
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
                Password = provider.Password,
                FullName = provider.FullName,
                Email = provider.Email,
                PhoneNumber = provider.PhoneNumber
            };
        }
    }
}
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityContracts.BindingModels;
using UniversityDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityDatabaseImplement.Implements
{
    public class CustomerStorage : ICustomerStorage
    {
        public List<CustomerViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Customers
                .Select(rec => new CustomerViewModel
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Phone = rec.Phone,
                    Login = rec.Login,
                    Password = rec.Password
                })
                .ToList();
        }
        public List<CustomerViewModel> GetFilteredList(CustomerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Customers
            .Include(rec => rec.Flows)
            .Include(rec => rec.Groups)
            .Include(rec => rec.Subjects)
            .Where(rec => rec.Email == model.Email)
            .Select(CreateModel)
            .ToList();
        }
        public CustomerViewModel GetElement(CustomerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var customer = context.Customers
            .Include(rec => rec.Flows)
            .Include(rec => rec.Groups)
            .Include(rec => rec.Subjects)
            .FirstOrDefault(rec => rec.Email == model.Email || rec.Id == model.Id);
            return customer != null ? CreateModel(customer) : null;
        }
        public void Insert(CustomerBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Customers.Add(CreateModel(model, new Customer()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(CustomerBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Customers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(CustomerBindingModel model)
        {
            using var context = new UniversityDatabase();
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Customers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Customer CreateModel(CustomerBindingModel model, Customer customer)
        {
            customer.Login = model.Login;
            customer.Name = model.Name;
            customer.Password = model.Password;
            customer.Phone = model.Phone;
            customer.Email = model.Email;
            return customer;
        }
        private static CustomerViewModel CreateModel(Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Password = customer.Password,
                Phone = customer.Phone,
                Email = customer.Email

            };
        }
    }
}

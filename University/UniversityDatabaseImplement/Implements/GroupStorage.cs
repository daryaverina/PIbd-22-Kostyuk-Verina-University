using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityContracts.BindingModels;
using UniversityDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityDatabaseImplement.Implements
{
    public class GroupStorage : IGroupStorage
    {
        public List<GroupViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Groups
            .Include(rec => rec.GroupDecrees)
            .ThenInclude(rec => rec.Decree)
            .Select(CreateModel)
            .ToList();
        }
        public List<GroupViewModel> GetFilteredList(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Groups
            .Include(rec => rec.GroupDecrees)
            .ThenInclude(rec => rec.Decree)
            .Where(rec => rec.Speciality.Contains(model.Speciality))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }
        public GroupViewModel GetElement(GroupBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var group = context.Groups
            .Include(rec => rec.GroupDecrees)
            .ThenInclude(rec => rec.Decree)
            .FirstOrDefault(rec => rec.Speciality == model.Speciality || rec.Id == model.Id);
            return group != null ? CreateModel(group) : null;
        }
        public void Insert(GroupBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Group group = new Group()
                {
                    Speciality = model.Speciality,
                    SemestersAmount = model.SemestersAmount,
                    DateCreated = model.DateCreated,
                    CustomerID = (int)model.CustomerID
                };
                context.Groups.Add(group);
                context.SaveChanges();
                CreateModel(model, group, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(GroupBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(GroupBindingModel model)
        {
            using var context = new UniversityDatabase();
            Group element = context.Groups.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Groups.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Group CreateModel(GroupBindingModel model, Group group, UniversityDatabase context)
        {
            group.Speciality = model.Speciality;
            group.SemestersAmount = (int)model.SemestersAmount; //TODO: надо?
            group.DateCreated = model.DateCreated;
            if (model.Id.HasValue)
            {
                var clientLoanPrograms = context.ClientLoanPrograms.Where(rec => rec.ClientId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.ClientLoanPrograms.RemoveRange(clientLoanPrograms.Where(rec => !model.ClientLoanPrograms.ContainsKey(rec.LoanProgramId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateLoanProgram in clientLoanPrograms)
                {
                    updateLoanProgram.Count = model.ClientLoanPrograms[updateLoanProgram.LoanProgramId].Item2;
                    model.ClientLoanPrograms.Remove(updateLoanProgram.LoanProgramId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var clp in model.ClientLoanPrograms)
            {
                context.ClientLoanPrograms.Add(new ClientLoanProgram
                {
                    ClientId = client.Id,
                    LoanProgramId = clp.Key,
                    Count = clp.Value.Item2
                });
                context.SaveChanges();
            }
            return group;
        }
        private static GroupViewModel CreateModel(Group group)
        {
            return new GroupViewModel
            {
                Id = group.Id,
                Speciality = group.Speciality,
                SemestersAmount = group.SemestersAmount,
                DateCreated = group.DateCreated
                .ToDictionary(recCLP => recCLP.LoanProgramId, recCLP => (recCLP.LoanProgram?.LoanProgramName, recCLP.Count))
            };
        }
    }
}
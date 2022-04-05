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
            group.SemestersAmount = model.SemestersAmount;
            group.DateCreated = model.DateCreated;
            group.CustomerID = (int)model.CustomerID;

            // StudentFlows реализуется на стороне даши ???

            if (model.Id.HasValue)
            {
                var groupDecrees = context.DecreeGroups.Where(rec => rec.GroupId == model.Id.Value).ToList();

                // Удалили те, которых нет в модели
                context.DecreeGroups.RemoveRange(groupDecrees.Where(rec => !model.DecreeGroups.ContainsKey(rec.DecreeId)).ToList());
                context.SaveChanges();
            }

            // Добавили новые
            foreach (var ds in model.DecreeGroups)
            {
                context.DecreeGroups.Add(new DecreeGroup
                {
                    GroupId = group.Id,
                    DecreeId = ds.Key,
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
            };
        }
    }
}
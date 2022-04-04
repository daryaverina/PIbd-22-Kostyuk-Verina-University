using UniversityContracts.BindingModels;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityDatabaseImplement.Implements
{
    public class SubjectStorage : ISubjectStorage
    {
        public void Delete(SubjectBindingModel model)
        {
            using var context = new UniversityDatabase();
            Subject element = context.Subjects.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Subjects.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Предмет не найден");
            }
        }

        public SubjectViewModel GetElement(SubjectBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var order = context.Subjects
            .Include(rec => rec.SubjectFlows)
            .ThenInclude(rec => rec.Flow)
            .Include(rec => rec.Customer)
            .FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public List<SubjectViewModel> GetFilteredList(SubjectBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Subjects
           .Include(rec => rec.SubjectFlows)
            .ThenInclude(rec => rec.Flow)
            .Include(rec => rec.Customer)
            .Where(rec => rec.Id == model.Id)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<SubjectViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Subjects
           .Include(rec => rec.SubjectFlows)
            .ThenInclude(rec => rec.Flow)
            .Include(rec => rec.Customer)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(SubjectBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Subjects.Add(CreateModel(model, new Subject()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(SubjectBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Subjects.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Дисциплина не найдена");
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
        private static Subject CreateModel(SubjectBindingModel model, Subject subject)
        {
            subject.Id = (int)model.Id;
            subject.SubjectName = (int)model.SubjectName;
            subject.HoursAmount = model.HoursAmount;
            return subject;
        }
        private static SubjectViewModel CreateModel(Subject subject)
        {
            return new SubjectViewModel
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
                HoursAmount = subject.HoursAmount
            };
        }
    }
}
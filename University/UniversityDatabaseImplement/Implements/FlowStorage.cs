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
    public class FlowStorage : IFlowStorage
    {
        public void Delete(FlowBindingModel model)
        {
            using var context = new UniversityDatabase();
            Flow element = context.Flows.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element != null)
            {
                context.Flows.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Поток не найден");
            }

        }

        public FlowViewModel GetElement(FlowBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            var flow = context.Flows
            .Include(rec => rec.FlowSubjects)
            .ThenInclude(rec => rec.Subject)
            .FirstOrDefault(rec => rec.Faculty == model.Faculty ||
            rec.Id == model.Id);
            return flow != null ? CreateModel(flow) : null;
        }

        public List<FlowViewModel> GetFilteredList(FlowBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new UniversityDatabase();
            return context.Flows
            .Include(rec => rec.FlowSubjects)
            .ThenInclude(rec => rec.Subject)
            .Where(rec => rec.Faculty.Contains(model.Faculty))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<FlowViewModel> GetFullList()
        {
            using var context = new UniversityDatabase();
            return context.Flows
            .Include(rec => rec.FlowSubjects)
            .ThenInclude(rec => rec.Subject)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(FlowBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Flow flow = new Flow()
                {
                    Faculty = model.Faculty,
                    NumberOfCourse = model.NumberOfCourse,
                    CustomerID = (int)model.CustomerID
                };
                context.Flows.Add(flow);
                context.SaveChanges();
                CreateModel(model, flow);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(FlowBindingModel model)
        {
            using var context = new UniversityDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Flows.FirstOrDefault(rec => rec.Id ==
                model.Id);
                if (element == null)
                {
                    throw new Exception("Поток не найден");
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
        private static Flow CreateModel(FlowBindingModel model, Flow flow)
        {
            flow.Faculty = model.Faculty;
            flow.NumberOfCourse = (int)model.NumberOfCourse;
            flow.CustomerID = (int)model.CustomerID;
            return flow;
        }

        private static FlowViewModel CreateModel(Flow flow)
        {
            //выбор групп по потокам
            return new FlowViewModel
            {
                Id = flow.Id,
                Faculty = flow.Faculty,
                Speciality = flow.Group.Speciality,
                NumberOfCourse = flow.NumberOfCourse

            };
        }
    }
}
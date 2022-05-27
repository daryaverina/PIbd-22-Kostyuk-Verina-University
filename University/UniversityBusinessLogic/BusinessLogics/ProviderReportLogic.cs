using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityContracts.BindingModels;
using UniversityContracts.StorageContracts;
using UniversityContracts.ViewModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityBusinessLogic.OfficePackage;
using UniversityBusinessLogic.OfficePackage.HelperModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    // получение списка дисциплин, посещаемых выбранными студентами, в формате doc/xls;
    // получение отчета по записям статуса обучения студентов на указанном потоке за определенный период в формате pdf на почту или его вывод на форме.

    public class ProviderReportLogic : IProviderReportLogic
    {
        private readonly ISubjectStorage _subjectStorage;
        private readonly IStudentStorage _studentStorage;
        private readonly IEducationalStatusStorage _statusStorage;
        private readonly IFlowStorage _flowStorage;
        private readonly ProviderAbstractSaveToWord _saveToWord;
        private readonly ProviderAbstractSaveToExcel _saveToExcel;
        private readonly ProviderAbstractSaveToPdf _saveToPdf;

        public ProviderReportLogic(ISubjectStorage subjectStorage, IStudentStorage studentStorage,
            IEducationalStatusStorage statusStorage, IFlowStorage flowStorage,
            ProviderAbstractSaveToWord saveToWord,
            ProviderAbstractSaveToExcel saveToExcel, ProviderAbstractSaveToPdf saveToPdf)
        {
            _subjectStorage = subjectStorage;
            _studentStorage = studentStorage;
            _statusStorage = statusStorage;
            _flowStorage = flowStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        // получение списка дисциплин, посещаемых выбранными студентами
        public List<ReportSubjectStudentViewModel> GetSubjectStudent(List<StudentViewModel> students)//
        {
            var list = new List<ReportSubjectStudentViewModel>();

            foreach (var student in students)
            {
                var record = new ReportSubjectStudentViewModel
                {
                    FullName = student.FullName,
                    FlowName = "",
                    Subjects = new List<Tuple<string>>()
                };
                foreach (var sf in student.StudentFlows)
                {
                    var model = _flowStorage.GetElement(new FlowBindingModel { Id = sf.Key });
                    foreach (var subject in model.SubjectFlows)
                    {
                        var s = new Tuple<string>(subject.Value);
                        if (!record.Subjects.Contains(s))
                        {
                            record.Subjects.Add(s);
                            record.FlowName = model.Faculty;
                        }
                    }
                }
                list.Add(record);
            }
            return list;
        }

        // получение отчета по записям статуса обучения студентов на указанном потоке за определенный период
        //public List<ReportStatusesViewModel> GetStatuses(ReportBindingModel model, int customerID)//
        //{
            
        //}

        // Сохранение дисциплин с указанием студентов в файл-Word
        public void SaveSubjectsToWordFile(ProviderReportBindingModel model)//
        {
            _saveToWord.CreateDoc(new ProviderWordInfo
            {
                FileName = model.FileName,
                Title = "Список дисциплин с указанием студентов",
                SubjectStudents = GetSubjectStudent(model.Students)
            });
        }

        // Сохранение дисциплин с указанием студентов в файл-Excel
        public void SaveSubjectsToExcelFile(ProviderReportBindingModel model)//
        {
            _saveToExcel.CreateReport(new ProviderExcelInfo
            {
                FileName = model.FileName,
                Title = "Список дисциплин с указанием студентов",
                SubjectStudents = GetSubjectStudent(model.Students)
            });
        }

        // Сохранение статусов обучения с указанием потоков в файл-Pdf
        public void SaveStatusesToPdfFile(ReportBindingModel model, int customerID)//
        {
            _saveToPdf.CreateDoc(new ProviderPdfInfo
            {
                FileName = model.FileName,
                Title = "Отчет по статусам обучения",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Statuses = GetStatuses(model, customerID)
            });
        }
    }
}

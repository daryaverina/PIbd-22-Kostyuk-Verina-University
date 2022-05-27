using UniversityContracts.BindingModels;
using UniversityContracts.BusinessLogicsContracts;
using UniversityBusinessLogic.MailWorker;
using UniversityContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UniversityRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IProviderReportLogic _report;

        private readonly ISubjectLogic _subject;

        private readonly IEducationalStatusLogic _status;

        private readonly MailKitWorker _mailWorker;

        public ReportController(IProviderReportLogic report, IEducationalStatusLogic status, ISubjectLogic subject, MailKitWorker mailWorker)
        {
            _report = report;
            _subject = subject;
            _status = status;
            _mailWorker = mailWorker;
        }

        [HttpPost]
        public void MakeWordFile(ProviderReportBindingModel model) => _report.SaveSubjectsToWordFile(model);

        [HttpPost]
        public void MakePdfFile(ProviderReportBindingModel model) => _report.SaveStatusesToPdfFile(model);

        [HttpPost]
        public void MakeExcelFile(ProviderReportBindingModel model) => _report.SaveSubjectsToExcelFile(model);

        [HttpPost]
        public void SendMail(ProviderReportBindingModel model)
        {
            _report.SaveStatusesToPdfFile(model);
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel
            {
                MailAddress = model.ProviderEmail,
                Subject = "Отчет университета 'Вы отчислены'",
                Text = $"Отчет по записям статуса обучения студентов на потоке с {model.DateFrom} по {model.DateTo}",
                ReportFile = model.FileName
            }); 
        }

        [HttpGet]
        public ProviderReportBindingModel GetStatuses(int providerId)
        {
            return new ProviderReportBindingModel
            {
                StatusIds = _status.Read(new EducationalStatusBindingModel { ProviderId = providerId }).Select(rec => rec.Id).ToList()
            };
        }
    }
}
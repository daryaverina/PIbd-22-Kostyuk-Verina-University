using UniversityContracts.BindingModels;
using UniversityContracts.ViewModels;

namespace UniversityContracts.BusinessLogicsContracts
{
    // получение списка дисциплин, посещаемых выбранными студентами, в формате doc/xls;
    // получение отчета по записям статуса обучения студентов на указанном потоке за определенный период в формате pdf на почту или его вывод на форме.

    public interface IProviderReportLogic
    {
        // Получение списка дисциплин с указанием студентов
        List<ReportSubjectStudentViewModel> GetSubjectStudent();

        // Получение списка статусов обучения с указанием потоков
        List<ReportStatusesViewModel> GetStatuses(ProviderReportBindingModel model);

        // Сохранение дисциплин с указанием студентов в файл-Word
        void SaveSubjectsToWordFile(ProviderReportBindingModel model);

        // Сохранение дисциплин с указанием студентов в файл-Excel
        void SaveSubjectsToExcelFile(ProviderReportBindingModel model);

        // Сохранение статусов обучения с указанием потоков в файл-Pdf
        void SaveStatusesToPdfFile(ProviderReportBindingModel model);
    }
}

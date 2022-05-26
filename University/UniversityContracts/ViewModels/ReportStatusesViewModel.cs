namespace UniversityContracts.ViewModels
{
    // получение отчета по записям статуса обучения студентов на указанном потоке за определенный период в формате pdf на почту или его вывод на форме.

    public class ReportStatusesViewModel
    {
        public DateTime DateCreate { get; set; }

        // форма обучения
        public string FStatus { get; set; }
        
        // основа так сказать база
        public string BStatus { get; set; }

        public string StudentFullName { get; set; }

        public string FlowName { get; set; }
    }
}

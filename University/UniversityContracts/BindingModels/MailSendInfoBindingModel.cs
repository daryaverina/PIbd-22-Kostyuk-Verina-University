namespace UniversityContracts.BindingModels
{
    public class MailSendInfoBindingModel
    {
        public string MailAddress { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        // файл отчета (адрес мли сам файл?)
        public string ReportFile { get; set; }
    }
}
using UniversityBusinessLogic.OfficePackage.HelperModels;
using UniversityBusinessLogic.OfficePackage.HelperEnums;

namespace UniversityBusinessLogic.OfficePackage
{
    public abstract class ProviderAbstractSaveToPdf
    {
        public void CreateDoc(ProviderPdfInfo info)
        {
            CreatePdf(info);

            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph
            {
                Text = $"с{ info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });

            CreateTable(new List<string> { "5cm", "5cm", "5cm", "5cm", "3cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата создания", "Форма обучения", "Основа обучения", "ФИО студента", "Поток" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var status in info.Statuses)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { status.DateCreate.ToShortDateString(), status.FStatus,
                        status.BStatus, status.StudentFullName, status.FlowName},
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }

            SavePdf(info);
        }

        // Создание pdf-файла
        protected abstract void CreatePdf(ProviderPdfInfo info);

        // Создание параграфа с текстом
        protected abstract void CreateParagraph(PdfParagraph paragraph);

        // Создание таблицы
        protected abstract void CreateTable(List<string> columns);

        // Создание и заполнение строки
        protected abstract void CreateRow(PdfRowParameters rowParameters);

        // Сохранение файла
        protected abstract void SavePdf(ProviderPdfInfo info);
    }
}
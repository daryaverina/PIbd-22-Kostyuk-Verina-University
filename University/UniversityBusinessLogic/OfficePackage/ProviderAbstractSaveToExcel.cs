using UniversityBusinessLogic.OfficePackage.HelperEnums;
using UniversityBusinessLogic.OfficePackage.HelperModels;

namespace UniversityBusinessLogic.OfficePackage
{
    public abstract class ProviderAbstractSaveToExcel
    {
        // Создание отчета
        public void CreateReport(ProviderExcelInfo info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;
            foreach (var ss in info.SubjectStudents)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = ss.SubjectName,
                    StyleInfo = ExcelStyleInfoType.Text
                });

                rowIndex++;

                foreach (var student in ss.Students)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = student.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });

                    rowIndex++;
                }
                rowIndex++;
            }

            SaveExcel(info);
        }

        // Создание excel-файла
        protected abstract void CreateExcel(ProviderExcelInfo info);

        // Добавляем новую ячейку в лист
        protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

        // Объединение ячеек
        protected abstract void MergeCells(ExcelMergeParameters excelParams);

        // Сохранение файла
        protected abstract void SaveExcel(ProviderExcelInfo info);
    }
}
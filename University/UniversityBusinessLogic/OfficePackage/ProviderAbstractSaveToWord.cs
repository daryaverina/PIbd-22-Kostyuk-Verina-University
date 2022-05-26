using UniversityBusinessLogic.OfficePackage.HelperEnums;
using UniversityBusinessLogic.OfficePackage.HelperModels;

namespace UniversityBusinessLogic.OfficePackage
{
    public abstract class ProviderAbstractSaveToWord
    {
        public void CreateDoc(ProviderWordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            foreach (var ss in info.SubjectStudents)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { 
                        ("      " + ss.SubjectName, new WordTextProperties { Bold = true, Size = "24", }) 
                    },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });

                int studentNumber = 1;

                foreach (var student in ss.Students)
                {
                    CreateParagraph(new WordParagraph
                    {
                        Texts = new List<(string, WordTextProperties)>
                        {
                            (studentNumber + ". " + student.Item1, new WordTextProperties { Size = "24", Bold = false })
                        },
                        TextProperties = new WordTextProperties
                        {
                            Size = "24",
                            JustificationType = WordJustificationType.Both
                        }
                    });
                }
            }

            SaveWord(info);
        }

        // Создание doc-файла
        protected abstract void CreateWord(ProviderWordInfo info);

        // Создание абзаца с текстом
        protected abstract void CreateParagraph(WordParagraph paragraph);

        // Сохранение файла
        protected abstract void SaveWord(ProviderWordInfo info);
    }
}
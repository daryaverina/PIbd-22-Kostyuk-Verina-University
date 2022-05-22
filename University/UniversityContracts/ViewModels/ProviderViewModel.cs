using UniversityContracts.Attributes;

namespace UniversityContracts.ViewModels
{
    // Поставщик
    public class ProviderViewModel
    {
        public int Id { get; set; }

        [Column(title: "ФИО", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string FullName { get; set; }

        [Column(title: "Электронная почта", width: 200)]
        public string Email { get; set; }

        [Column(title: "Пароль", width: 150)]
        public string Password { get; set; }

        [Column(title: "Номер телефона", width: 150)]
        public string PhoneNumber { get; set; }
    }
}

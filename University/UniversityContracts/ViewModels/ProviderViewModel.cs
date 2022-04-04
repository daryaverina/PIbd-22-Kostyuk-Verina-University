using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    // Поставщик
    public class ProviderViewModel
    {
        public int Id { get; set; }

        [DisplayName("Логин")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("ФИО")]
        public string FullName { get; set; }

        [DisplayName("Электронная почта")]
        public string Email { get; set; }

        [DisplayName("Номер телефона")]
        public string PhoneNumber { get; set; }
    }
}

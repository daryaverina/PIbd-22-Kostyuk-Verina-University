using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace UniversityContracts.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [DisplayName("Логин")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Почта")]
        public string Email { get; set; }

        [DisplayName("Номер телефона")]
        public string Phone { get; set; }

    }
}

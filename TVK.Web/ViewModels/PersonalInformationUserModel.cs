using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TVK.Web.ViewModels
{
    public class PersonalInformationUserModel
    {

        [Required(ErrorMessage = "Не указан Пол пользователя")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Не указан Возвраст пользователя")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Не указано Имя пользователя")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Не указана Фамилия пользователя")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Не указано Отчество пользователя")]
        public string Secondname { get; set; }

    }
}

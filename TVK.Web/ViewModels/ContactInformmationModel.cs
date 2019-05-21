using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TVK.Web.ViewModels
{
    public class ContactInformmationModel
    {

        [Required(ErrorMessage = "Не указан дополнительный телефон или дополнительная почта.")]
        public string PhoneOrEmail { get; set; }

        [Required(ErrorMessage = "Не указан комментарий.")]
        public string Comment { get; set; }
    }
}

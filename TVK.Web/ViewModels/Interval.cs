using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TVK.Web.ViewModels
{
    public class Interval
    {
        [Required(ErrorMessage = "Не указана начальная дата")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Не указана конечная дата")]
        public string FinishDate { get; set; }

        [Required(ErrorMessage = "Не указан IP адрес")]
        public string IP_Address_loadpercentage { get; set; }


        [Required(ErrorMessage = "Не указан IP адрес")]
        public string IP_Address_freepsycisal { get; set; }
        
    }
}

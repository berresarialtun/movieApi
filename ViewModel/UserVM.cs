using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.ViewModel
{
    public class UserVM
    {
        
        public string username { get; set; }

        public string name { get; set; }

        public string surname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [Required, DataType(DataType.Password)]
        public string password { get; set; }

    }
}

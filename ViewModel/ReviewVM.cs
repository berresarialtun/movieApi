using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.ViewModel
{
    public class ReviewVM
    {
        public User User { get; set; }
        public Movie Movie { get; set; }
        public string Comment { get; set; }
        public int Vote { get; set; }
    }
}
